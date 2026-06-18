// AdminForm.cs
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
using OfficeOpenXml;

namespace NetMail
{
    public partial class AdminForm : Form
    {
        // Scheduler token
        private CancellationTokenSource _scheduleCts;

        // Nhiều file đính kèm
        private readonly List<string> _attachments = new List<string>();
        private const long MaxAttachmentSizeBytes = 20 * 1024 * 1024; // 20MB

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

        public AdminForm()
        {
            InitializeComponent();

            // EPPlus license
            ExcelPackage.License.SetNonCommercialPersonal("NetMail Admin");

            // Scheduler defaults
            try
            {
                if (this.dtpScheduledTime != null)
                {
                    dtpScheduledTime.Format = DateTimePickerFormat.Custom;
                    dtpScheduledTime.CustomFormat = "dd/MM/yyyy HH:mm";
                }
                if (this.nudIntervalMinutes != null)
                {
                    nudIntervalMinutes.Minimum = 0;
                    nudIntervalMinutes.Maximum = 100000;
                    nudIntervalMinutes.Value = 0;
                }
                if (this.lblScheduleStatus != null)
                {
                    lblScheduleStatus.Text = "Schedule: stopped";
                }
            }
            catch { }

            // Lịch sử gửi
            try
            {
                if (dgvHistory != null)
                {
                    dgvHistory.AutoGenerateColumns = true;
                    dgvHistory.DataSource = _history;
                    dgvHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgvHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    dgvHistory.CellFormatting += dgvHistory_CellFormatting;
                }
            }
            catch { }

            // Drag & drop cho ô đính kèm
            try
            {
                if (txtAttachment != null)
                {
                    txtAttachment.AllowDrop = true;
                    txtAttachment.Multiline = true;
                    txtAttachment.ScrollBars = ScrollBars.Vertical;
                    txtAttachment.DragEnter += txtAttachment_DragEnter;
                    txtAttachment.DragDrop += txtAttachment_DragDrop;
                }
            }
            catch { }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        #region User Management (unchanged logic)
        private void LoadUsers()
        {
            try
            {
                dgvUsers.Rows.Clear();
                var users = DatabaseHelper.GetAllUsers();

                if (users == null) return;

                foreach (var u in users)
                {
                    dgvUsers.Rows.Add(u.Id, u.Email, u.IsVerified ? "Yes" : "No", u.IsAdmin ? "Yes" : "No");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadUsers();

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["colId"].Value);
            string email = row.Cells["colEmail"].Value.ToString();

            var r = MessageBox.Show($"Delete user {email}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                if (DatabaseHelper.DeleteUser(id))
                {
                    MessageBox.Show("Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else MessageBox.Show("Delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToggleVerification_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["colId"].Value);

            if (DatabaseHelper.ToggleUserVerification(id))
            {
                MessageBox.Show("Toggled verification.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else MessageBox.Show("Operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["colId"].Value);
            string email = row.Cells["colEmail"].Value.ToString();

            string newPass = PromptForPassword($"Set new password for {email}:");
            if (string.IsNullOrEmpty(newPass)) return;

            if (DatabaseHelper.ResetUserPassword(id, newPass))
            {
                MessageBox.Show("Password reset.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Reset failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string PromptForPassword(string prompt)
        {
            using (Form dlg = new Form())
            {
                dlg.Width = 400; dlg.Height = 150; dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent; dlg.MaximizeBox = false; dlg.MinimizeBox = false;
                var lbl = new Label() { Left = 12, Top = 12, Width = 360, Text = prompt };
                var tb = new TextBox() { Left = 12, Top = 36, Width = 360, UseSystemPasswordChar = true };
                var ok = new Button() { Text = "OK", Left = 220, Width = 70, Top = 70, DialogResult = DialogResult.OK };
                var cancel = new Button() { Text = "Cancel", Left = 300, Width = 70, Top = 70, DialogResult = DialogResult.Cancel };
                dlg.Controls.Add(lbl); dlg.Controls.Add(tb); dlg.Controls.Add(ok); dlg.Controls.Add(cancel);
                dlg.AcceptButton = ok; dlg.CancelButton = cancel;
                return dlg.ShowDialog() == DialogResult.OK ? tb.Text : string.Empty;
            }
        }
        #endregion

        #region Parsing & Validation helpers (To/CC/BCC)
        private List<string> ParseRawAddresses(string raw)
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(raw)) return list;
            var parts = raw.Split(new[] { '\n', '\r', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in parts)
            {
                var s = p.Trim();
                if (!string.IsNullOrEmpty(s)) list.Add(s);
            }
            return list;
        }

        private List<string> GetInvalidAddresses(IEnumerable<string> addresses)
        {
            var invalid = new List<string>();
            foreach (var a in addresses)
            {
                try { var _ = new MailAddress(a); }
                catch { invalid.Add(a); }
            }
            return invalid;
        }

        // Trả về danh sách email bị trùng (xuất hiện > 1 lần trong To + Cc + Bcc)
        private List<string> GetDuplicateAddresses(IEnumerable<string> addresses)
        {
            return addresses
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a.Trim())
                .GroupBy(a => a, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
        }

        // Kiểm tra xem email gửi (txtEmail) có nằm trong danh sách người nhận không
        private bool SenderAppearsInRecipients(string senderEmail, IEnumerable<string> addresses)
        {
            if (string.IsNullOrWhiteSpace(senderEmail)) return false;
            string normSender = senderEmail.Trim().ToLowerInvariant();
            return addresses.Any(a => a.Trim().ToLowerInvariant() == normSender);
        }

        private void RemoveAddressesFromTextbox(TextBox tb, List<string> toRemove)
        {
            if (tb == null || string.IsNullOrWhiteSpace(tb.Text)) return;
            var parts = tb.Text.Split(new[] { '\n', '\r', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .Where(x => !toRemove.Contains(x))
                        .ToList();
            tb.Text = string.Join(Environment.NewLine, parts);
        }

        private void btnValidateRecipients_Click(object sender, EventArgs e)
        {
            lstInvalid.Items.Clear();

            // Tách riêng từng nhóm để sau này còn dùng nếu cần
            var toList = ParseRawAddresses(txtTo.Text);
            var ccList = ParseRawAddresses(txtCc.Text);
            var bccList = ParseRawAddresses(txtBcc.Text);

            var all = new List<string>();
            all.AddRange(toList);
            all.AddRange(ccList);
            all.AddRange(bccList);

            // 1) Lỗi cú pháp / không đúng rule
            var invalidSyntax = GetInvalidAddresses(all);

            // 2) Trùng lặp giữa To + Cc + Bcc
            var duplicates = GetDuplicateAddresses(all);

            // 3) Người nhận trùng với email gửi (txtEmail)
            string senderEmail = txtEmail.Text.Trim();
            bool senderInRecipients = SenderAppearsInRecipients(senderEmail, all);

            // Gom tất cả "problem emails" vào 1 tập hợp cho gọn
            var problems = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var a in invalidSyntax)
                problems.Add(a);

            foreach (var a in duplicates)
                problems.Add(a);

            if (senderInRecipients && !string.IsNullOrWhiteSpace(senderEmail))
                problems.Add(senderEmail);

            if (problems.Count == 0)
            {
                MessageBox.Show(
                    "All addresses look valid and there are no duplicates.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                foreach (var s in problems)
                    lstInvalid.Items.Add(s);

                MessageBox.Show(
                    $"Found {problems.Count} invalid / duplicate address(es). " +
                    "See the list and remove or fix them.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }


        private void btnRemoveSelectedInvalid_Click(object sender, EventArgs e)
        {
            if (lstInvalid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select invalid addresses to remove.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var toRemove = lstInvalid.SelectedItems.Cast<string>().ToList();
            RemoveAddressesFromTextbox(txtTo, toRemove);
            RemoveAddressesFromTextbox(txtCc, toRemove);
            RemoveAddressesFromTextbox(txtBcc, toRemove);

            foreach (var s in toRemove) lstInvalid.Items.Remove(s);
            MessageBox.Show("Removed selected invalid addresses.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Helper chung (sending, attachment, history)
        private void SetSendingState(bool isSending)
        {
            try
            {
                this.UseWaitCursor = isSending;
                btnSend.Enabled = !isSending;
                btnStartSchedule.Enabled = !isSending;
                btnStopSchedule.Enabled = !isSending;
            }
            catch { }
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
                    // OK
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
        #endregion

        #region Sending (async) - used by manual Send and scheduler
        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendEmailAsync();
        }

        private async Task SendEmailAsync()
        {
            SetSendingState(true);

            try
            {
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string smtpServer = txtSmtpServer.Text.Trim();
                if (string.IsNullOrEmpty(smtpServer))
                {
                    MessageBox.Show("SMTP server required.");
                    LogSentMail("No SMTP");
                    return;
                }

                if (!int.TryParse(txtPort.Text.Trim(), out int port))
                {
                    MessageBox.Show("Port invalid.");
                    LogSentMail("Invalid port");
                    return;
                }

                bool useSsl = chkSSL.Checked;
                string username = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string subject = txtSubject.Text.Trim();
                string body = txtMessage.Text ?? string.Empty;

                var toList = ParseRawAddresses(txtTo.Text);
                var ccList = ParseRawAddresses(txtCc.Text);
                var bccList = ParseRawAddresses(txtBcc.Text);

                var all = toList.Concat(ccList).Concat(bccList).ToList();
                if (all.Count == 0)
                {
                    MessageBox.Show("At least one recipient required.");
                    LogSentMail("No recipients");
                    return;
                }

                var invalid = GetInvalidAddresses(all);
                if (invalid.Count > 0)
                {
                    lstInvalid.Items.Clear();
                    foreach (var s in invalid) lstInvalid.Items.Add(s);
                    MessageBox.Show($"Found {invalid.Count} invalid address(es). Fix before sending.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogSentMail("Invalid recipients");
                    return;
                }

                using (var client = new SmtpClient(smtpServer))
                {
                    client.Port = port;
                    client.EnableSsl = useSsl;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    client.Timeout = 60000;

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(username);
                        foreach (var a in toList) message.To.Add(new MailAddress(a));
                        foreach (var a in ccList) message.CC.Add(new MailAddress(a));
                        foreach (var a in bccList) message.Bcc.Add(new MailAddress(a));

                        message.Subject = subject;
                        message.Body = body;

                        foreach (var path in _attachments)
                        {
                            if (File.Exists(path))
                            {
                                message.Attachments.Add(new Attachment(path));
                            }
                        }

                        // Retry logic giống User
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
                MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException smtpEx)
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("SMTP error: " + smtpEx.Message);
                if (smtpEx.InnerException != null) sb.AppendLine("Inner: " + smtpEx.InnerException.Message);
                MessageBox.Show(sb.ToString(), "SMTP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ " + smtpEx.Message;
                LogSentMail("SMTP Error: " + smtpEx.Message);
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
        #endregion

        #region Browse / Clear / Logout
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        AddAttachment(file);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSmtpServer.Clear();
            txtPort.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtTo.Clear();
            txtCc.Clear();
            txtBcc.Clear();
            txtSubject.Clear();
            txtMessage.Clear();
            chkSSL.Checked = false;
            lblStatus.Text = "Ready";
            lstInvalid.Items.Clear();

            _attachments.Clear();
            RefreshAttachmentTextbox();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Logout?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                Hide();
                var login = new LoginForm();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }
        #endregion

        #region Scheduler (Start / Stop)
        private void btnStartSchedule_Click(object sender, EventArgs e)
        {
            if (_scheduleCts != null)
            {
                MessageBox.Show("Schedule already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtpScheduledTime == null || nudIntervalMinutes == null)
            {
                MessageBox.Show("Scheduler controls not present in Designer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime firstRun = dtpScheduledTime.Value;
            int intervalMinutes = (int)nudIntervalMinutes.Value; // 0 = no repeat

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
                        if (lblScheduleStatus != null)
                            lblScheduleStatus.Text = $"Schedule running. Next: {nextRun:dd/MM/yyyy HH:mm}";
                    }));

                    while (!token.IsCancellationRequested)
                    {
                        TimeSpan delay = nextRun - DateTime.Now;
                        if (delay > TimeSpan.Zero)
                        {
                            await Task.Delay(delay, token);
                        }

                        if (token.IsCancellationRequested) break;

                        try
                        {
                            this.Invoke((Action)(() =>
                            {
                                if (lblScheduleStatus != null)
                                    lblScheduleStatus.Text = $"Sending at {DateTime.Now:dd/MM/yyyy HH:mm:ss} .";
                            }));

                            await RunOnUiThreadAsync(SendEmailAsync);

                            this.Invoke((Action)(() =>
                            {
                                if (lblScheduleStatus != null)
                                    lblScheduleStatus.Text = $"Last sent: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                            }));
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception exSend)
                        {
                            this.Invoke((Action)(() =>
                            {
                                if (lblScheduleStatus != null)
                                    lblScheduleStatus.Text = $"Error: {exSend.Message}";
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
                                if (lblScheduleStatus != null)
                                    lblScheduleStatus.Text = $"Schedule running. Next: {nextRun:dd/MM/yyyy HH:mm}";
                            }));
                        }
                        else
                        {
                            this.Invoke((Action)(() =>
                            {
                                if (lblScheduleStatus != null)
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
                        if (lblScheduleStatus != null && lblScheduleStatus.Text.StartsWith("Schedule running"))
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
                if (lblScheduleStatus != null) lblScheduleStatus.Text = "Schedule stopping...";
                MessageBox.Show("Schedule stopped.", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Schedule is not running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Excel import & History resend
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
            return null;
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

                        TextBox targetBox = AskImportTargetTextbox();
                        if (targetBox == null)
                        {
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
            txtAttachment.Text = "";

            if (!string.IsNullOrWhiteSpace(log.Attachments))
            {
                var paths = log.Attachments
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .ToList();

                foreach (var p in paths)
                {
                    AddAttachment(p);
                }
            }

            await SendEmailAsync();
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var col = dgvHistory.Columns[e.ColumnIndex];

            if (col.DataPropertyName == "To" ||
                col.DataPropertyName == "Cc" ||
                col.DataPropertyName == "Bcc")
            {
                if (e.Value != null)
                {
                    e.Value = e.Value.ToString().Replace(";", Environment.NewLine);
                    e.FormattingApplied = true;
                }
            }
        }
        #endregion

        #region Drag & drop attachment
        private void txtAttachment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtAttachment_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var f in files)
                {
                    AddAttachment(f);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi drag & drop file:\n" + ex.Message, "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
