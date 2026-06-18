// User.Designer.cs
namespace NetMail
{
    partial class User
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblSmtp = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.grpMailContent = new System.Windows.Forms.GroupBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblCc = new System.Windows.Forms.Label();
            this.txtCc = new System.Windows.Forms.TextBox();
            this.lblBcc = new System.Windows.Forms.Label();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtAttachment = new System.Windows.Forms.TextBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dtpScheduledTime = new System.Windows.Forms.DateTimePicker();
            this.nudIntervalMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblInterval = new System.Windows.Forms.Label();
            this.btnStartSchedule = new System.Windows.Forms.Button();
            this.btnStopSchedule = new System.Windows.Forms.Button();
            this.lblScheduleStatus = new System.Windows.Forms.Label();
            this.btnValidateRecipients = new System.Windows.Forms.Button();
            this.lstInvalidRecipients = new System.Windows.Forms.ListBox();
            this.btnRemoveInvalidRecipients = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnResendSelected = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadFromExcel = new System.Windows.Forms.Button();
            this.grpMailContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalMinutes)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSmtp
            // 
            this.lblSmtp.AutoSize = true;
            this.lblSmtp.BackColor = System.Drawing.Color.Transparent;
            this.lblSmtp.Location = new System.Drawing.Point(18, 16);
            this.lblSmtp.Name = "lblSmtp";
            this.lblSmtp.Size = new System.Drawing.Size(94, 20);
            this.lblSmtp.TabIndex = 0;
            this.lblSmtp.Text = "SMTP Server:";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSmtpServer.Location = new System.Drawing.Point(118, 12);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(430, 27);
            this.txtSmtpServer.TabIndex = 1;
            this.txtSmtpServer.Text = "smtp.gmail.com";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.Location = new System.Drawing.Point(661, 16);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(38, 20);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(705, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 27);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "587";
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkSSL.Checked = true;
            this.chkSSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSSL.Location = new System.Drawing.Point(781, 14);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(82, 24);
            this.chkSSL.TabIndex = 4;
            this.chkSSL.Text = "Use SSL";
            this.chkSSL.UseVisualStyleBackColor = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Location = new System.Drawing.Point(18, 58);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(49, 20);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(118, 54);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(330, 27);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Location = new System.Drawing.Point(575, 57);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(545, 54);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(334, 27);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // grpMailContent
            // 
            this.grpMailContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMailContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.grpMailContent.Controls.Add(this.lblTo);
            this.grpMailContent.Controls.Add(this.txtTo);
            this.grpMailContent.Controls.Add(this.lblCc);
            this.grpMailContent.Controls.Add(this.txtCc);
            this.grpMailContent.Controls.Add(this.lblBcc);
            this.grpMailContent.Controls.Add(this.txtBcc);
            this.grpMailContent.Controls.Add(this.lblSubject);
            this.grpMailContent.Controls.Add(this.txtSubject);
            this.grpMailContent.Controls.Add(this.lblMessage);
            this.grpMailContent.Controls.Add(this.txtMessage);
            this.grpMailContent.Controls.Add(this.txtAttachment);
            this.grpMailContent.Controls.Add(this.lblAttachment);
            this.grpMailContent.Controls.Add(this.btnBrowse);
            this.grpMailContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpMailContent.Location = new System.Drawing.Point(18, 92);
            this.grpMailContent.Name = "grpMailContent";
            this.grpMailContent.Padding = new System.Windows.Forms.Padding(10);
            this.grpMailContent.Size = new System.Drawing.Size(1015, 469);
            this.grpMailContent.TabIndex = 9;
            this.grpMailContent.TabStop = false;
            this.grpMailContent.Text = "Mail Content";
            this.grpMailContent.Enter += new System.EventHandler(this.grpMailContent_Enter);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(18, 32);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(28, 20);
            this.lblTo.TabIndex = 0;
            this.lblTo.Text = "To:";
            // 
            // txtTo
            // 
            this.txtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTo.Location = new System.Drawing.Point(82, 29);
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTo.Size = new System.Drawing.Size(470, 90);
            this.txtTo.TabIndex = 1;
            // 
            // lblCc
            // 
            this.lblCc.AutoSize = true;
            this.lblCc.Location = new System.Drawing.Point(676, 32);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(30, 20);
            this.lblCc.TabIndex = 2;
            this.lblCc.Text = "CC:";
            // 
            // txtCc
            // 
            this.txtCc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCc.Location = new System.Drawing.Point(607, 29);
            this.txtCc.Multiline = true;
            this.txtCc.Name = "txtCc";
            this.txtCc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCc.Size = new System.Drawing.Size(388, 60);
            this.txtCc.TabIndex = 3;
            // 
            // lblBcc
            // 
            this.lblBcc.AutoSize = true;
            this.lblBcc.Location = new System.Drawing.Point(676, 100);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(39, 20);
            this.lblBcc.TabIndex = 4;
            this.lblBcc.Text = "BCC:";
            // 
            // txtBcc
            // 
            this.txtBcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcc.Location = new System.Drawing.Point(607, 96);
            this.txtBcc.Multiline = true;
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBcc.Size = new System.Drawing.Size(388, 60);
            this.txtBcc.TabIndex = 5;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(18, 133);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(61, 20);
            this.lblSubject.TabIndex = 6;
            this.lblSubject.Text = "Subject:";
            this.lblSubject.Click += new System.EventHandler(this.lblSubject_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(82, 129);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(470, 27);
            this.txtSubject.TabIndex = 7;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(18, 169);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(70, 20);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Message:";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(22, 192);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(973, 190);
            this.txtMessage.TabIndex = 9;
            // 
            // txtAttachment
            // 
            this.txtAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttachment.Location = new System.Drawing.Point(113, 392);
            this.txtAttachment.Multiline = true;
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAttachment.Size = new System.Drawing.Size(570, 64);
            this.txtAttachment.TabIndex = 11;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.BackColor = System.Drawing.Color.Transparent;
            this.lblAttachment.Location = new System.Drawing.Point(18, 549);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(89, 20);
            this.lblAttachment.TabIndex = 10;
            this.lblAttachment.Text = "Attachment:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(692, 392);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 29);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dtpScheduledTime
            // 
            this.dtpScheduledTime.Location = new System.Drawing.Point(20, 37);
            this.dtpScheduledTime.Name = "dtpScheduledTime";
            this.dtpScheduledTime.Size = new System.Drawing.Size(230, 27);
            this.dtpScheduledTime.TabIndex = 13;
            this.dtpScheduledTime.ValueChanged += new System.EventHandler(this.dtpScheduledTime_ValueChanged);
            // 
            // nudIntervalMinutes
            // 
            this.nudIntervalMinutes.Location = new System.Drawing.Point(124, 70);
            this.nudIntervalMinutes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudIntervalMinutes.Name = "nudIntervalMinutes";
            this.nudIntervalMinutes.Size = new System.Drawing.Size(70, 27);
            this.nudIntervalMinutes.TabIndex = 15;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.BackColor = System.Drawing.Color.Transparent;
            this.lblInterval.Location = new System.Drawing.Point(19, 73);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(98, 20);
            this.lblInterval.TabIndex = 14;
            this.lblInterval.Text = "Repeat (min):";
            // 
            // btnStartSchedule
            // 
            this.btnStartSchedule.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStartSchedule.Location = new System.Drawing.Point(19, 105);
            this.btnStartSchedule.Name = "btnStartSchedule";
            this.btnStartSchedule.Size = new System.Drawing.Size(70, 30);
            this.btnStartSchedule.TabIndex = 16;
            this.btnStartSchedule.Text = "Start";
            this.btnStartSchedule.UseVisualStyleBackColor = false;
            this.btnStartSchedule.Click += new System.EventHandler(this.btnStartSchedule_Click);
            // 
            // btnStopSchedule
            // 
            this.btnStopSchedule.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStopSchedule.Location = new System.Drawing.Point(97, 105);
            this.btnStopSchedule.Name = "btnStopSchedule";
            this.btnStopSchedule.Size = new System.Drawing.Size(70, 30);
            this.btnStopSchedule.TabIndex = 17;
            this.btnStopSchedule.Text = "Stop";
            this.btnStopSchedule.UseVisualStyleBackColor = false;
            this.btnStopSchedule.Click += new System.EventHandler(this.btnStopSchedule_Click);
            // 
            // lblScheduleStatus
            // 
            this.lblScheduleStatus.AutoSize = true;
            this.lblScheduleStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblScheduleStatus.Location = new System.Drawing.Point(19, 138);
            this.lblScheduleStatus.Name = "lblScheduleStatus";
            this.lblScheduleStatus.Size = new System.Drawing.Size(131, 20);
            this.lblScheduleStatus.TabIndex = 18;
            this.lblScheduleStatus.Text = "Schedule: stopped";
            // 
            // btnValidateRecipients
            // 
            this.btnValidateRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidateRecipients.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnValidateRecipients.Location = new System.Drawing.Point(860, 567);
            this.btnValidateRecipients.Name = "btnValidateRecipients";
            this.btnValidateRecipients.Size = new System.Drawing.Size(161, 37);
            this.btnValidateRecipients.TabIndex = 19;
            this.btnValidateRecipients.Text = "Validate / Preview";
            this.btnValidateRecipients.UseVisualStyleBackColor = false;
            this.btnValidateRecipients.Click += new System.EventHandler(this.btnValidateRecipients_Click);
            // 
            // lstInvalidRecipients
            // 
            this.lstInvalidRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstInvalidRecipients.ItemHeight = 20;
            this.lstInvalidRecipients.Location = new System.Drawing.Point(19, 248);
            this.lstInvalidRecipients.Name = "lstInvalidRecipients";
            this.lstInvalidRecipients.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstInvalidRecipients.Size = new System.Drawing.Size(372, 164);
            this.lstInvalidRecipients.TabIndex = 20;
            this.lstInvalidRecipients.SelectedIndexChanged += new System.EventHandler(this.lstInvalidRecipients_SelectedIndexChanged);
            // 
            // btnRemoveInvalidRecipients
            // 
            this.btnRemoveInvalidRecipients.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRemoveInvalidRecipients.Location = new System.Drawing.Point(19, 418);
            this.btnRemoveInvalidRecipients.Name = "btnRemoveInvalidRecipients";
            this.btnRemoveInvalidRecipients.Size = new System.Drawing.Size(174, 39);
            this.btnRemoveInvalidRecipients.TabIndex = 21;
            this.btnRemoveInvalidRecipients.Text = "Remove Selected ";
            this.btnRemoveInvalidRecipients.UseVisualStyleBackColor = false;
            this.btnRemoveInvalidRecipients.Click += new System.EventHandler(this.btnRemoveInvalidRecipients_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSend.Location = new System.Drawing.Point(911, 610);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 36);
            this.btnSend.TabIndex = 22;
            this.btnSend.Text = "Send Email";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.btnClear.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnClear.Location = new System.Drawing.Point(18, 567);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 36);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear all";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLogout.Location = new System.Drawing.Point(210, 605);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(90, 36);
            this.btnLogout.TabIndex = 24;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 657);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1435, 26);
            this.statusStrip.TabIndex = 25;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 20);
            this.lblStatus.Text = "Ready";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgvHistory);
            this.panel1.Controls.Add(this.btnResendSelected);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpScheduledTime);
            this.panel1.Controls.Add(this.nudIntervalMinutes);
            this.panel1.Controls.Add(this.lblInterval);
            this.panel1.Controls.Add(this.lblScheduleStatus);
            this.panel1.Controls.Add(this.btnStopSchedule);
            this.panel1.Controls.Add(this.btnStartSchedule);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.lstInvalidRecipients);
            this.panel1.Controls.Add(this.btnRemoveInvalidRecipients);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(1039, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 657);
            this.panel1.TabIndex = 26;
            // 
            // dgvHistory
            // 
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(19, 450);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.Size = new System.Drawing.Size(372, 150);
            this.dgvHistory.TabIndex = 30;
            // 
            // btnResendSelected
            // 
            this.btnResendSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResendSelected.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnResendSelected.Location = new System.Drawing.Point(19, 605);
            this.btnResendSelected.Name = "btnResendSelected";
            this.btnResendSelected.Size = new System.Drawing.Size(174, 36);
            this.btnResendSelected.TabIndex = 29;
            this.btnResendSelected.Text = "Resend Selected";
            this.btnResendSelected.UseVisualStyleBackColor = false;
            this.btnResendSelected.Click += new System.EventHandler(this.btnResendSelected_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(19, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Invalid addresses:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Schedule:";
            // 
            // btnLoadFromExcel
            // 
            this.btnLoadFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFromExcel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadFromExcel.Location = new System.Drawing.Point(675, 567);
            this.btnLoadFromExcel.Name = "btnLoadFromExcel";
            this.btnLoadFromExcel.Size = new System.Drawing.Size(179, 37);
            this.btnLoadFromExcel.TabIndex = 27;
            this.btnLoadFromExcel.Text = "Load from Excel";
            this.btnLoadFromExcel.UseVisualStyleBackColor = false;
            this.btnLoadFromExcel.Click += new System.EventHandler(this.btnLoadFromExcel_Click);
            // 
            // User
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1435, 683);
            this.Controls.Add(this.btnLoadFromExcel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSmtp);
            this.Controls.Add(this.txtSmtpServer);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.chkSSL);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.grpMailContent);
            this.Controls.Add(this.btnValidateRecipients);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 640);
            this.Name = "User";
            this.Text = "User - NetMail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.User_Load);
            this.grpMailContent.ResumeLayout(false);
            this.grpMailContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalMinutes)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSmtp;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox chkSSL;

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.GroupBox grpMailContent;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.TextBox txtCc;
        private System.Windows.Forms.Label lblBcc;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;

        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.TextBox txtAttachment;
        private System.Windows.Forms.Button btnBrowse;

        private System.Windows.Forms.DateTimePicker dtpScheduledTime;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown nudIntervalMinutes;
        private System.Windows.Forms.Button btnStartSchedule;
        private System.Windows.Forms.Button btnStopSchedule;
        private System.Windows.Forms.Label lblScheduleStatus;

        private System.Windows.Forms.Button btnValidateRecipients;
        private System.Windows.Forms.ListBox lstInvalidRecipients;
        private System.Windows.Forms.Button btnRemoveInvalidRecipients;

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnResendSelected;
        private System.Windows.Forms.Button btnLoadFromExcel;
        private System.Windows.Forms.TextBox txtBcc;
    }
}
