// User.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using WinFormsTimer = System.Windows.Forms.Timer;


namespace NetMail
{
    public partial class User : Form
    {
        private CancellationTokenSource _scheduleCts;

        //Gửi nhiều file đính kèm
        private readonly List<string> _attachments = new List<string>();
        private const long MaxAttachmentSizeBytes = 20 * 1024 * 1024; 

        // Countdown cho scheduler
        private DateTime? _nextRun;
        private WinFormsTimer _countdownTimer;

        // Lịch sử gửi
        public class SentMailLog
        {
            public DateTime Time { get; set; }
            public string To { get; set; }
            public string Cc { get; set; }
            public string Bcc { get; set; }
            public string Subject { get; set; }
            public string Attachments { get; set; }
            public string Status { get; set; }
        }

        private readonly BindingList<SentMailLog> _history = new BindingList<SentMailLog>();

        public User()
        {
            InitializeComponent();

            // EPPlus license
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("NetMail User");

            lblScheduleStatus.Text = "Schedule: stopped";
            dtpScheduledTime.Format = DateTimePickerFormat.Custom;
            dtpScheduledTime.CustomFormat = "dd/MM/yyyy HH:mm";
            nudIntervalMinutes.Minimum = 0;
            nudIntervalMinutes.Maximum = 100000;
            nudIntervalMinutes.Value = 0;

            // Countdown timer
            _countdownTimer = new WinFormsTimer();
            _countdownTimer.Interval = 1000;
            _countdownTimer.Tick += CountdownTimer_Tick;

            // Lịch sử gửi
            dgvHistory.AutoGenerateColumns = true;
            dgvHistory.DataSource = _history;
            dgvHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;           
            dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;          
            dgvHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;  
            dgvHistory.CellFormatting += dgvHistory_CellFormatting;

            // Drag & drop cho ô đính kèm
            txtAttachment.AllowDrop = true;
            txtAttachment.Multiline = true;
            txtAttachment.ScrollBars = ScrollBars.Vertical;
            txtAttachment.DragEnter += txtAttachment_DragEnter;
            txtAttachment.DragDrop += txtAttachment_DragDrop;
        }

        // -------------------- Helper chung --------------------

        private void SetSendingState(bool isSending)
        {
            try
            {
                this.UseWaitCursor = isSending;
                btnSend.Enabled = !isSending;
                btnStartSchedule.Enabled = !isSending;
                btnStopSchedule.Enabled = !isSending;
            }
            catch
            {
                // tránh crash nếu form dispose
            }
        }

        private void RefreshAttachmentTextbox()
        {
            txtAttachment.Text = string.Join(Environment.NewLine, _attachments);
        }

        private void AddAttachment(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show($"File không tồn tại: {path}", "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var fi = new FileInfo(path);
                if (fi.Length > MaxAttachmentSizeBytes)
                {
                    MessageBox.Show($"File quá lớn (>20MB): {fi.Name}", "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra lock
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // nếu vào được tới đây là OK
                }

                if (!_attachments.Contains(path))
                {
                    _attachments.Add(path);
                    RefreshAttachmentTextbox();
                }
            }
            catch (IOException)
            {
                MessageBox.Show($"File đang được sử dụng (bị khóa): {path}", "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm file đính kèm:\n{ex.Message}", "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsTransient(SmtpException ex)
        {
            switch (ex.StatusCode)
            {
                case SmtpStatusCode.MailboxBusy:
                case SmtpStatusCode.MailboxUnavailable:
                case SmtpStatusCode.ServiceNotAvailable:
                case SmtpStatusCode.TransactionFailed:
                case SmtpStatusCode.GeneralFailure:
                    return true;
                default:
                    return false;
            }
        }

        private void LogSentMail(string status)
        {
            var log = new SentMailLog
            {
                Time = DateTime.Now,
                To = string.Join(";", ParseRawAddresses(txtTo.Text)),
                Cc = string.Join(";", ParseRawAddresses(txtCc.Text)),
                Bcc = string.Join(";", ParseRawAddresses(txtBcc.Text)),
                Subject = txtSubject.Text.Trim(),
                Attachments = string.Join(";", _attachments),
                Status = status
            };

            _history.Add(log);
        }

        private List<string> LoadEmailsFromExcel(string filePath, int columnIndex = 1, int startRow = 2)
        {
            var result = new List<string>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets[0]; 
                int row = startRow;
                while (true)
                {
                    var val = ws.Cells[row, columnIndex].Text;
                    if (string.IsNullOrWhiteSpace(val))
                        break;

                    result.Add(val.Trim());
                    row++;
                }
            }

            return result;
        }

        // Chạy 1 hàm async trên UI thread (dùng cho scheduler)
        private Task RunOnUiThreadAsync(Func<Task> func)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (this.IsDisposed)
            {
                tcs.SetResult(false);
                return tcs.Task;
            }

            this.BeginInvoke(new Action(async () =>
            {
                try
                {
                    await func();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }));

            return tcs.Task;
        }

        // -------------------- Xử lý địa chỉ --------------------

        // Parse raw addresses (splits on newlines, commas, semicolons)
        private List<string> ParseRawAddresses(string raw)
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(raw)) return list;
            var parts = raw.Split(new[] { ',', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in parts)
            {
                var addr = p.Trim();
                if (!string.IsNullOrEmpty(addr)) list.Add(addr);
            }
            return list;
        }

        // Gom tất cả địa chỉ từ To, Cc, Bcc
        private List<string> GetAllRecipients()
        {
            var all = new List<string>();
            all.AddRange(ParseRawAddresses(txtTo.Text));
            all.AddRange(ParseRawAddresses(txtCc.Text));
            all.AddRange(ParseRawAddresses(txtBcc.Text));
            return all;
        }

        //kiểm tra mail
        private static readonly Regex EmailRegex = new Regex(
            @"^[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+" +
            @"@" +
            @"[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?" +
            @"(?:\.[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?)+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private bool IsValidEmail(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return false;

            address = address.Trim();

            if (address.Length > 320) return false;

            int atIndex = address.IndexOf('@');
            if (atIndex <= 0 || atIndex == address.Length - 1)
                return false;

            string local = address.Substring(0, atIndex);
            string domain = address.Substring(atIndex + 1);

            if (local.Length == 0 || local.Length > 64) return false;
            if (domain.Length == 0 || domain.Length > 255) return false;

            if (local.StartsWith(".") || local.EndsWith(".")) return false;
            if (domain.StartsWith(".") || domain.EndsWith(".")) return false;

            if (local.Contains("..") || domain.Contains("..")) return false;

            if (!EmailRegex.IsMatch(address)) return false;

            try
            {
                var _ = new MailAddress(address);
            }
            catch
            {
                return false;
            }

            if (!domain.Equals("gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
        //kiểm tra xem mail có trùng với người gửi không
        private bool IsSenderIncludedInRecipients()
        {
            string sender = txtEmail.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(sender)) return false;

            var all = GetAllRecipients()
                .Select(x => x.Trim().ToLowerInvariant())
                .ToList();

            return all.Contains(sender);
        }

        // Return invalid addresses (that MailAddress cannot parse)
        private List<string> GetInvalidAddresses(IEnumerable<string> addresses)
        {
            var invalid = new List<string>();

            foreach (var a in addresses)
            {
                var addr = a?.Trim();
                if (string.IsNullOrEmpty(addr)) continue;

                if (!IsValidEmail(addr))
                    invalid.Add(addr);
            }

            return invalid;
        }

        // Validate / Preview button handler
        private void btnValidateRecipients_Click(object sender, EventArgs e)
        {
            lstInvalidRecipients.Items.Clear();
            var all = GetAllRecipients();

            var invalid = GetInvalidAddresses(all).Distinct().ToList();

            // Duplicate filter
            var duplicates = all
                .Select(x => x.Trim().ToLowerInvariant())
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            // Sender included in recipients
            string senderEmail = txtEmail.Text.Trim().ToLowerInvariant();
            bool senderExist = all.Any(r => r.Trim().ToLowerInvariant() == senderEmail);

            if (senderExist)
            {
                lstInvalidRecipients.Items.Add(senderEmail);
            }


            foreach (var s in invalid) lstInvalidRecipients.Items.Add(s);
            foreach (var d in duplicates) lstInvalidRecipients.Items.Add(d);

            if (lstInvalidRecipients.Items.Count == 0)
            {
                MessageBox.Show("All addresses look valid and there are no duplicates or sender conflicts.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Detected errors. Fix or remove emails listed as invalid.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Remove selected invalid addresses from To/Cc/Bcc
        private void btnRemoveInvalidRecipients_Click(object sender, EventArgs e)
        {
            if (lstInvalidRecipients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select invalid addresses to remove.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var toRemove = lstInvalidRecipients.SelectedItems.Cast<string>().ToList();
            RemoveAddressesFromTextbox(txtTo, toRemove);
            RemoveAddressesFromTextbox(txtCc, toRemove);
            RemoveAddressesFromTextbox(txtBcc, toRemove);

            foreach (var s in toRemove) lstInvalidRecipients.Items.Remove(s);

            MessageBox.Show("Selected invalid addresses removed.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RemoveAddressesFromTextbox(TextBox tb, List<string> addressesToRemove)
        {
            if (tb == null || string.IsNullOrWhiteSpace(tb.Text)) return;
            var parts = tb.Text.Split(new[] { '\n', '\r', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => x.Trim())
                               .Where(x => !addressesToRemove.Contains(x))
                               .ToList();
            tb.Text = string.Join(Environment.NewLine, parts);
        }

        // -------------------- Gửi email --------------------

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendEmailAsync();
        }
        //hàm gửi mail
        private async Task SendEmailAsync()
        {
            try
            {
                SetSendingState(true);
                lblStatus.Text = "Đang gửi email...";

                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string smtpServer = txtSmtpServer.Text.Trim();
                if (string.IsNullOrEmpty(smtpServer)) throw new InvalidOperationException("SMTP server is required.");
                if (!int.TryParse(txtPort.Text.Trim(), out int port)) throw new InvalidOperationException("Port must be a number.");

                bool useSsl = chkSSL.Checked;
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string subject = txtSubject.Text.Trim();
                string body = txtMessage.Text.Trim();

                var toList = ParseRawAddresses(txtTo.Text);
                var ccList = ParseRawAddresses(txtCc.Text);
                var bccList = ParseRawAddresses(txtBcc.Text);

                var all = toList.Concat(ccList).Concat(bccList).ToList();
                if (all.Count == 0)
                {
                    MessageBox.Show("Please enter at least one recipient.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogSentMail("No recipients");
                    return;
                }

                var invalid = GetInvalidAddresses(all);
                if (invalid.Count > 0)
                {
                    lstInvalidRecipients.Items.Clear();
                    foreach (var s in invalid) lstInvalidRecipients.Items.Add(s);
                    MessageBox.Show($"Found {invalid.Count} invalid address(es). Fix or remove before sending.", "Invalid Addresses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogSentMail("Invalid recipients");
                    return;
                }

                // Kiểm tra email trùng
                var duplicates = all
                    .Select(a => a.Trim())
                    .Where(a => !string.IsNullOrEmpty(a))
                    .GroupBy(a => a.ToLowerInvariant())
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicates.Count > 0)
                {
                    MessageBox.Show(
                        "Some addresses appear more than once in To/Cc/Bcc. " +
                        "Please remove duplicates before sending.\n\n" +
                        "Duplicated:\n - " + string.Join("\n - ", duplicates),
                        "Duplicate Addresses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Sender included?
                string senderEmail = txtEmail.Text.Trim().ToLowerInvariant();
                if (all.Any(r => r.Trim().ToLowerInvariant() == senderEmail))
                {
                    lstInvalidRecipients.Items.Add(senderEmail);
                    MessageBox.Show("Sender email found in recipients. Remove it before sending.",
                        "Duplicate Sender", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                if (IsSenderIncludedInRecipients())
                {
                    MessageBox.Show(
                        "You cannot send an email to your own address. Remove your email from recipients.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }


                using (var client = new SmtpClient(smtpServer))
                {
                    client.Port = port;
                    client.EnableSsl = useSsl;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(email, password);
                    client.Timeout = 60000;

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(email);
                        foreach (var a in toList) message.To.Add(new MailAddress(a));
                        foreach (var a in ccList) message.CC.Add(new MailAddress(a));
                        foreach (var a in bccList) message.Bcc.Add(new MailAddress(a));

                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        foreach (var path in _attachments)
                        {
                            if (File.Exists(path))
                            {
                                message.Attachments.Add(new Attachment(path));
                            }
                        }

                        // Retry logic
                        int maxRetries = 3;
                        int delaySeconds = 3;
                        bool sent = false;
                        Exception lastEx = null;

                        for (int attempt = 1; attempt <= maxRetries && !sent; attempt++)
                        {
                            try
                            {
                                await client.SendMailAsync(message);
                                sent = true;
                            }
                            catch (SmtpException ex) when (IsTransient(ex) && attempt < maxRetries)
                            {
                                lastEx = ex;
                                await Task.Delay(TimeSpan.FromSeconds(delaySeconds * attempt));
                            }
                        }

                        if (!sent && lastEx != null)
                        {
                            throw lastEx;
                        }
                    }
                }

                lblStatus.Text = $"✅ Email sent: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                LogSentMail("Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ " + ex.Message;
                LogSentMail("Error: " + ex.Message);
            }
            finally
            {
                SetSendingState(false);
            }
        }

        // Browse attachment (multiple)
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Multiselect = true;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in open.FileNames)
                    {
                        AddAttachment(file);
                    }
                }
            }
        }
        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Lấy tên cột theo DataPropertyName (vì bạn AutoGenerateColumns)
            var col = dgvHistory.Columns[e.ColumnIndex];

            if (col.DataPropertyName == "To" ||
                col.DataPropertyName == "Cc" ||
                col.DataPropertyName == "Bcc")
            {
                if (e.Value != null)
                {
                    // đổi dấu ; thành xuống dòng
                    e.Value = e.Value.ToString().Replace(";", Environment.NewLine);
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTo.Clear();
            txtCc.Clear();
            txtBcc.Clear();
            txtSubject.Clear();
            txtMessage.Clear();
            chkSSL.Checked = true;
            lblStatus.Text = "Ready";
            lstInvalidRecipients.Items.Clear();

            _attachments.Clear();
            RefreshAttachmentTextbox();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Confirm Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }

        // -------------------- Scheduler --------------------

        private void btnStartSchedule_Click(object sender, EventArgs e)
        {
            if (_scheduleCts != null)
            {
                MessageBox.Show("Schedule is already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime firstRun = dtpScheduledTime.Value;
            int intervalMinutes = (int)nudIntervalMinutes.Value;

            if (firstRun <= DateTime.Now)
            {
                if (intervalMinutes > 0)
                {
                    TimeSpan diff = DateTime.Now - firstRun;
                    long steps = (long)(diff.TotalMinutes / Math.Max(1, intervalMinutes)) + 1;
                    firstRun = firstRun.AddMinutes(steps * intervalMinutes);
                }
                else firstRun = DateTime.Now.AddSeconds(1);
            }

            _scheduleCts = new CancellationTokenSource();
            CancellationToken token = _scheduleCts.Token;

            Task.Run(async () =>
            {
                try
                {
                    DateTime nextRun = firstRun;
                    this.Invoke((Action)(() =>
                    {
                        _nextRun = nextRun;
                        _countdownTimer.Start();
                        lblScheduleStatus.Text = $"Schedule running. Next: {nextRun:dd/MM/yyyy HH:mm}";
                    }));

                    while (!token.IsCancellationRequested)
                    {
                        TimeSpan delay = nextRun - DateTime.Now;
                        if (delay > TimeSpan.Zero) await Task.Delay(delay, token);

                        if (token.IsCancellationRequested) break;

                        try
                        {
                            this.Invoke((Action)(() =>
                            {
                                lblScheduleStatus.Text = $"Sending at {DateTime.Now:dd/MM/yyyy HH:mm:ss} ...";
                            }));

                            await RunOnUiThreadAsync(SendEmailAsync);

                            this.Invoke((Action)(() =>
                            {
                                lblScheduleStatus.Text = $"Last sent: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                            }));
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception exSend)
                        {
                            this.Invoke((Action)(() =>
                            {
                                lblScheduleStatus.Text = $"Error while sending: {exSend.Message}";
                            }));
                        }

                        if (intervalMinutes > 0)
                        {
                            nextRun = nextRun.AddMinutes(intervalMinutes);
                            if (nextRun <= DateTime.Now)
                            {
                                double minsBehind = (DateTime.Now - nextRun).TotalMinutes;
                                long steps = (long)(minsBehind / intervalMinutes) + 1;
                                nextRun = nextRun.AddMinutes(steps * intervalMinutes);
                            }
                            this.Invoke((Action)(() =>
                            {
                                _nextRun = nextRun;
                                if (!_countdownTimer.Enabled)
                                    _countdownTimer.Start();
                                lblScheduleStatus.Text = $"Schedule running. Next: {nextRun:dd/MM/yyyy HH:mm}";
                            }));
                        }
                        else
                        {
                            this.Invoke((Action)(() =>
                            {
                                _nextRun = null;
                                _countdownTimer.Stop();
                                lblScheduleStatus.Text = $"Schedule finished (one-time send completed at {DateTime.Now:dd/MM/yyyy HH:mm:ss})";
                            }));
                            break;
                        }
                    }
                }
                catch (TaskCanceledException) { }
                finally
                {
                    _scheduleCts = null;
                    this.Invoke((Action)(() =>
                    {
                        _nextRun = null;
                        _countdownTimer.Stop();
                        if (lblScheduleStatus.Text.StartsWith("Schedule running"))
                            lblScheduleStatus.Text = "Schedule stopped";
                    }));
                }
            }, token);

            MessageBox.Show($"Schedule started. First run: {firstRun:dd/MM/yyyy HH:mm}", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStopSchedule_Click(object sender, EventArgs e)
        {
            if (_scheduleCts != null)
            {
                _scheduleCts.Cancel();
                _scheduleCts = null;
                lblScheduleStatus.Text = "Schedule stopping...";
                MessageBox.Show("Schedule stopped.", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Schedule is not running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (_nextRun.HasValue && _scheduleCts != null)
            {
                var remaining = _nextRun.Value - DateTime.Now;
                if (remaining <= TimeSpan.Zero)
                {
                    lblScheduleStatus.Text = "Đang gửi ngay...";
                    _countdownTimer.Stop();
                }
                else
                {
                    lblScheduleStatus.Text = $"Next send at {_nextRun:dd/MM/yyyy HH:mm} ({remaining:hh\\:mm\\:ss} còn lại)";
                }
            }
            else
            {
                _countdownTimer.Stop();
            }
        }

        // -------------------- Excel import & History resend --------------------
        private TextBox AskImportTargetTextbox()
        {
            using (var f = new FormSelectTarget())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    switch (f.SelectedTarget)
                    {
                        case "To": return txtTo;
                        case "Cc": return txtCc;
                        case "Bcc": return txtBcc;
                    }
                }
            }
            return null; // user đóng form hoặc không chọn
        }
        private void btnLoadFromExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel Files|*.xlsx;*.xls";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var emails = LoadEmailsFromExcel(open.FileName);
                        if (emails.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy email nào trong file.",
                                "Excel Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Hỏi import vào To / Cc / Bcc
                        TextBox targetBox = AskImportTargetTextbox();
                        if (targetBox == null)
                        {
                            // user đóng form hoặc không chọn gì
                            return;
                        }

                        var current = ParseRawAddresses(targetBox.Text);
                        current.AddRange(emails);
                        current = current
                            .Select(x => x.Trim())
                            .Where(x => !string.IsNullOrEmpty(x))
                            .Distinct(StringComparer.OrdinalIgnoreCase)
                            .ToList();

                        targetBox.Text = string.Join(Environment.NewLine, current);

                        MessageBox.Show($"Đã import {emails.Count} email từ Excel vào ô {targetBox.Name}.",
                            "Excel Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc Excel:\n" + ex.Message,
                            "Excel Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnResendSelected_Click(object sender, EventArgs e)
        {
            if (dgvHistory.CurrentRow == null)
            {
                MessageBox.Show("Chọn 1 dòng trong lịch sử để gửi lại.", "History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var log = dgvHistory.CurrentRow.DataBoundItem as SentMailLog;
            if (log == null) return;

            txtSubject.Text = log.Subject;
            txtTo.Text = log.To.Replace(";", Environment.NewLine);
            txtCc.Text = log.Cc.Replace(";", Environment.NewLine);
            txtBcc.Text = log.Bcc.Replace(";", Environment.NewLine);

            _attachments.Clear();
            txtAttachment.Text = ""; // Xóa trước khi gán lại

            if (!string.IsNullOrWhiteSpace(log.Attachments))
            {
                var paths = log.Attachments
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

                // Hiển thị từng file xuống dòng
                txtAttachment.Text = string.Join(Environment.NewLine, paths);

                foreach (var path in paths)
                {
                    AddAttachment(path);
                }
            }

            await SendEmailAsync();
        }

        //  Drag & drop attachment textbox

        private void txtAttachment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtAttachment_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                AddAttachment(file);
            }
        }

        //  Event trống 
        private void User_Load(object sender, EventArgs e) { }
        private void grpMailContent_Enter(object sender, EventArgs e) { }
        private void dtpScheduledTime_ValueChanged(object sender, EventArgs e) { }
        private void lstInvalidRecipients_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtSubject_TextChanged(object sender, EventArgs e) { }
        private void lblSubject_Click(object sender, EventArgs e) { }
    }
}
