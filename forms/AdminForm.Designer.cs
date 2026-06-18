// AdminForm.Designer.cs
namespace NetMail
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVerified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsAdmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelUsersButtons = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnToggleVerification = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabEmailSender = new System.Windows.Forms.TabPage();
            this.grpMailContent = new System.Windows.Forms.GroupBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtAttachment = new System.Windows.Forms.TextBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.lblBcc = new System.Windows.Forms.Label();
            this.txtCc = new System.Windows.Forms.TextBox();
            this.lblCc = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnResendSelected = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpScheduledTime = new System.Windows.Forms.DateTimePicker();
            this.btnStartSchedule = new System.Windows.Forms.Button();
            this.lblInterval = new System.Windows.Forms.Label();
            this.btnStopSchedule = new System.Windows.Forms.Button();
            this.nudIntervalMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblScheduleStatus = new System.Windows.Forms.Label();
            this.lstInvalid = new System.Windows.Forms.ListBox();
            this.btnRemoveInvalid = new System.Windows.Forms.Button();
            this.lblSmtp = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnLoadFromExcel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelUsersButtons.SuspendLayout();
            this.tabEmailSender.SuspendLayout();
            this.grpMailContent.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalMinutes)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Controls.Add(this.tabEmailSender);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1774, 856);
            this.tabControl.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.panelUsersButtons);
            this.tabUsers.Location = new System.Drawing.Point(4, 29);
            this.tabUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabUsers.Size = new System.Drawing.Size(1383, 641);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colEmail,
            this.colVerified,
            this.colIsAdmin});
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(3, 4);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(1111, 633);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Width = 80;
            // 
            // colEmail
            // 
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 6;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 250;
            // 
            // colVerified
            // 
            this.colVerified.HeaderText = "Verified";
            this.colVerified.MinimumWidth = 6;
            this.colVerified.Name = "colVerified";
            this.colVerified.ReadOnly = true;
            this.colVerified.Width = 125;
            // 
            // colIsAdmin
            // 
            this.colIsAdmin.HeaderText = "Admin";
            this.colIsAdmin.MinimumWidth = 6;
            this.colIsAdmin.Name = "colIsAdmin";
            this.colIsAdmin.ReadOnly = true;
            this.colIsAdmin.Width = 125;
            // 
            // panelUsersButtons
            // 
            this.panelUsersButtons.Controls.Add(this.btnLogout);
            this.panelUsersButtons.Controls.Add(this.btnResetPassword);
            this.panelUsersButtons.Controls.Add(this.btnToggleVerification);
            this.panelUsersButtons.Controls.Add(this.btnDeleteUser);
            this.panelUsersButtons.Controls.Add(this.btnRefresh);
            this.panelUsersButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelUsersButtons.Location = new System.Drawing.Point(1114, 4);
            this.panelUsersButtons.Name = "panelUsersButtons";
            this.panelUsersButtons.Padding = new System.Windows.Forms.Padding(10);
            this.panelUsersButtons.Size = new System.Drawing.Size(266, 633);
            this.panelUsersButtons.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLogout.Location = new System.Drawing.Point(23, 560);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 40);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnResetPassword.Location = new System.Drawing.Point(23, 200);
            this.btnResetPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(200, 40);
            this.btnResetPassword.TabIndex = 3;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = false;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // btnToggleVerification
            // 
            this.btnToggleVerification.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnToggleVerification.Location = new System.Drawing.Point(23, 140);
            this.btnToggleVerification.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnToggleVerification.Name = "btnToggleVerification";
            this.btnToggleVerification.Size = new System.Drawing.Size(200, 40);
            this.btnToggleVerification.TabIndex = 2;
            this.btnToggleVerification.Text = "Toggle Verify";
            this.btnToggleVerification.UseVisualStyleBackColor = false;
            this.btnToggleVerification.Click += new System.EventHandler(this.btnToggleVerification_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDeleteUser.Location = new System.Drawing.Point(23, 80);
            this.btnDeleteUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(200, 40);
            this.btnDeleteUser.TabIndex = 1;
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRefresh.Location = new System.Drawing.Point(23, 20);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 40);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Reload";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabEmailSender
            // 
            this.tabEmailSender.BackColor = System.Drawing.Color.Transparent;
            this.tabEmailSender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabEmailSender.BackgroundImage")));
            this.tabEmailSender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabEmailSender.Controls.Add(this.grpMailContent);
            this.tabEmailSender.Controls.Add(this.panel1);
            this.tabEmailSender.Controls.Add(this.lblSmtp);
            this.tabEmailSender.Controls.Add(this.txtSmtpServer);
            this.tabEmailSender.Controls.Add(this.lblPort);
            this.tabEmailSender.Controls.Add(this.txtPort);
            this.tabEmailSender.Controls.Add(this.chkSSL);
            this.tabEmailSender.Controls.Add(this.lblEmail);
            this.tabEmailSender.Controls.Add(this.txtEmail);
            this.tabEmailSender.Controls.Add(this.lblPassword);
            this.tabEmailSender.Controls.Add(this.txtPassword);
            this.tabEmailSender.Controls.Add(this.btnValidate);
            this.tabEmailSender.Controls.Add(this.btnLoadFromExcel);
            this.tabEmailSender.Controls.Add(this.btnSend);
            this.tabEmailSender.Controls.Add(this.btnClear);
            this.tabEmailSender.Location = new System.Drawing.Point(4, 29);
            this.tabEmailSender.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabEmailSender.Name = "tabEmailSender";
            this.tabEmailSender.Size = new System.Drawing.Size(1766, 823);
            this.tabEmailSender.TabIndex = 1;
            this.tabEmailSender.Text = "Email Sender";
            // 
            // grpMailContent
            // 
            this.grpMailContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMailContent.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grpMailContent.Controls.Add(this.lblTo);
            this.grpMailContent.Controls.Add(this.btnBrowse);
            this.grpMailContent.Controls.Add(this.txtAttachment);
            this.grpMailContent.Controls.Add(this.lblAttachment);
            this.grpMailContent.Controls.Add(this.txtMessage);
            this.grpMailContent.Controls.Add(this.lblMessage);
            this.grpMailContent.Controls.Add(this.txtSubject);
            this.grpMailContent.Controls.Add(this.lblSubject);
            this.grpMailContent.Controls.Add(this.txtBcc);
            this.grpMailContent.Controls.Add(this.lblBcc);
            this.grpMailContent.Controls.Add(this.txtCc);
            this.grpMailContent.Controls.Add(this.lblCc);
            this.grpMailContent.Controls.Add(this.txtTo);
            this.grpMailContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpMailContent.Location = new System.Drawing.Point(30, 83);
            this.grpMailContent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpMailContent.Name = "grpMailContent";
            this.grpMailContent.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpMailContent.Size = new System.Drawing.Size(1307, 616);
            this.grpMailContent.TabIndex = 9;
            this.grpMailContent.TabStop = false;
            this.grpMailContent.Text = "Mail Content";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(14, 42);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(28, 20);
            this.lblTo.TabIndex = 0;
            this.lblTo.Text = "To:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(730, 462);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(99, 29);
            this.btnBrowse.TabIndex = 22;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtAttachment
            // 
            this.txtAttachment.Location = new System.Drawing.Point(117, 463);
            this.txtAttachment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAttachment.Multiline = true;
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAttachment.Size = new System.Drawing.Size(589, 102);
            this.txtAttachment.TabIndex = 21;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Location = new System.Drawing.Point(17, 466);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(89, 20);
            this.lblAttachment.TabIndex = 20;
            this.lblAttachment.Text = "Attachment:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(114, 221);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(924, 224);
            this.txtMessage.TabIndex = 19;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(14, 221);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(70, 20);
            this.lblMessage.TabIndex = 18;
            this.lblMessage.Text = "Message:";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(114, 177);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(520, 27);
            this.txtSubject.TabIndex = 17;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(14, 181);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(61, 20);
            this.lblSubject.TabIndex = 16;
            this.lblSubject.Text = "Subject:";
            // 
            // txtBcc
            // 
            this.txtBcc.Location = new System.Drawing.Point(694, 125);
            this.txtBcc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBcc.Multiline = true;
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBcc.Size = new System.Drawing.Size(344, 77);
            this.txtBcc.TabIndex = 15;
            // 
            // lblBcc
            // 
            this.lblBcc.AutoSize = true;
            this.lblBcc.Location = new System.Drawing.Point(654, 148);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(39, 20);
            this.lblBcc.TabIndex = 14;
            this.lblBcc.Text = "BCC:";
            // 
            // txtCc
            // 
            this.txtCc.Location = new System.Drawing.Point(694, 39);
            this.txtCc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCc.Multiline = true;
            this.txtCc.Name = "txtCc";
            this.txtCc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCc.Size = new System.Drawing.Size(344, 78);
            this.txtCc.TabIndex = 13;
            // 
            // lblCc
            // 
            this.lblCc.AutoSize = true;
            this.lblCc.Location = new System.Drawing.Point(654, 43);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(30, 20);
            this.lblCc.TabIndex = 12;
            this.lblCc.Text = "CC:";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(114, 38);
            this.txtTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTo.Size = new System.Drawing.Size(520, 128);
            this.txtTo.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgvHistory);
            this.panel1.Controls.Add(this.btnResendSelected);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpScheduledTime);
            this.panel1.Controls.Add(this.btnStartSchedule);
            this.panel1.Controls.Add(this.lblInterval);
            this.panel1.Controls.Add(this.btnStopSchedule);
            this.panel1.Controls.Add(this.nudIntervalMinutes);
            this.panel1.Controls.Add(this.lblScheduleStatus);
            this.panel1.Controls.Add(this.lstInvalid);
            this.panel1.Controls.Add(this.btnRemoveInvalid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1363, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 823);
            this.panel1.TabIndex = 27;
            // 
            // dgvHistory
            // 
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(21, 632);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.Size = new System.Drawing.Size(367, 163);
            this.dgvHistory.TabIndex = 29;
            // 
            // btnResendSelected
            // 
            this.btnResendSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResendSelected.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnResendSelected.Location = new System.Drawing.Point(21, 592);
            this.btnResendSelected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResendSelected.Name = "btnResendSelected";
            this.btnResendSelected.Size = new System.Drawing.Size(175, 34);
            this.btnResendSelected.TabIndex = 28;
            this.btnResendSelected.Text = "Resend Selected";
            this.btnResendSelected.UseVisualStyleBackColor = false;
            this.btnResendSelected.Click += new System.EventHandler(this.btnResendSelected_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 25;
            this.label2.Text = "Invalid Addresses:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "Schedule:";
            // 
            // dtpScheduledTime
            // 
            this.dtpScheduledTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpScheduledTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduledTime.Location = new System.Drawing.Point(16, 41);
            this.dtpScheduledTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpScheduledTime.Name = "dtpScheduledTime";
            this.dtpScheduledTime.Size = new System.Drawing.Size(262, 27);
            this.dtpScheduledTime.TabIndex = 0;
            // 
            // btnStartSchedule
            // 
            this.btnStartSchedule.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStartSchedule.Location = new System.Drawing.Point(13, 115);
            this.btnStartSchedule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStartSchedule.Name = "btnStartSchedule";
            this.btnStartSchedule.Size = new System.Drawing.Size(70, 31);
            this.btnStartSchedule.TabIndex = 3;
            this.btnStartSchedule.Text = "Start";
            this.btnStartSchedule.UseVisualStyleBackColor = false;
            this.btnStartSchedule.Click += new System.EventHandler(this.btnStartSchedule_Click);
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(18, 84);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(102, 18);
            this.lblInterval.TabIndex = 1;
            this.lblInterval.Text = "Repeat (min):";
            // 
            // btnStopSchedule
            // 
            this.btnStopSchedule.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStopSchedule.Location = new System.Drawing.Point(89, 115);
            this.btnStopSchedule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStopSchedule.Name = "btnStopSchedule";
            this.btnStopSchedule.Size = new System.Drawing.Size(70, 31);
            this.btnStopSchedule.TabIndex = 4;
            this.btnStopSchedule.Text = "Stop";
            this.btnStopSchedule.UseVisualStyleBackColor = false;
            this.btnStopSchedule.Click += new System.EventHandler(this.btnStopSchedule_Click);
            // 
            // nudIntervalMinutes
            // 
            this.nudIntervalMinutes.Location = new System.Drawing.Point(126, 82);
            this.nudIntervalMinutes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudIntervalMinutes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudIntervalMinutes.Name = "nudIntervalMinutes";
            this.nudIntervalMinutes.Size = new System.Drawing.Size(70, 27);
            this.nudIntervalMinutes.TabIndex = 2;
            // 
            // lblScheduleStatus
            // 
            this.lblScheduleStatus.AutoSize = true;
            this.lblScheduleStatus.Location = new System.Drawing.Point(17, 154);
            this.lblScheduleStatus.Name = "lblScheduleStatus";
            this.lblScheduleStatus.Size = new System.Drawing.Size(131, 20);
            this.lblScheduleStatus.TabIndex = 5;
            this.lblScheduleStatus.Text = "Schedule: stopped";
            // 
            // lstInvalid
            // 
            this.lstInvalid.ItemHeight = 20;
            this.lstInvalid.Location = new System.Drawing.Point(22, 263);
            this.lstInvalid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstInvalid.Name = "lstInvalid";
            this.lstInvalid.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstInvalid.Size = new System.Drawing.Size(360, 164);
            this.lstInvalid.TabIndex = 23;
            // 
            // btnRemoveInvalid
            // 
            this.btnRemoveInvalid.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRemoveInvalid.Location = new System.Drawing.Point(21, 435);
            this.btnRemoveInvalid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveInvalid.Name = "btnRemoveInvalid";
            this.btnRemoveInvalid.Size = new System.Drawing.Size(156, 36);
            this.btnRemoveInvalid.TabIndex = 24;
            this.btnRemoveInvalid.Text = "Remove Selected";
            this.btnRemoveInvalid.UseVisualStyleBackColor = false;
            this.btnRemoveInvalid.Click += new System.EventHandler(this.btnRemoveSelectedInvalid_Click);
            // 
            // lblSmtp
            // 
            this.lblSmtp.AutoSize = true;
            this.lblSmtp.Location = new System.Drawing.Point(20, 18);
            this.lblSmtp.Name = "lblSmtp";
            this.lblSmtp.Size = new System.Drawing.Size(94, 20);
            this.lblSmtp.TabIndex = 0;
            this.lblSmtp.Text = "SMTP Server:";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(120, 14);
            this.txtSmtpServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(496, 27);
            this.txtSmtpServer.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(650, 19);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(38, 20);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(700, 15);
            this.txtPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 27);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "587";
            // 
            // chkSSL
            // 
            this.chkSSL.Location = new System.Drawing.Point(795, 16);
            this.chkSSL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(104, 24);
            this.chkSSL.TabIndex = 4;
            this.chkSSL.Text = "Use SSL";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 48);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(49, 20);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 44);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(496, 27);
            this.txtEmail.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(650, 49);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(739, 44);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(241, 27);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnValidate
            // 
            this.btnValidate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnValidate.Location = new System.Drawing.Point(890, 683);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(178, 36);
            this.btnValidate.TabIndex = 22;
            this.btnValidate.Text = "Validate / Preview";
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Click += new System.EventHandler(this.btnValidateRecipients_Click);
            // 
            // btnLoadFromExcel
            // 
            this.btnLoadFromExcel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadFromExcel.Location = new System.Drawing.Point(698, 683);
            this.btnLoadFromExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoadFromExcel.Name = "btnLoadFromExcel";
            this.btnLoadFromExcel.Size = new System.Drawing.Size(178, 36);
            this.btnLoadFromExcel.TabIndex = 21;
            this.btnLoadFromExcel.Text = "Load from Excel";
            this.btnLoadFromExcel.UseVisualStyleBackColor = false;
            this.btnLoadFromExcel.Click += new System.EventHandler(this.btnLoadFromExcel_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSend.Location = new System.Drawing.Point(948, 727);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 36);
            this.btnSend.TabIndex = 25;
            this.btnSend.Text = "Send Email";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.btnClear.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnClear.Location = new System.Drawing.Point(24, 725);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 36);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "Clear all";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 856);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1774, 26);
            this.statusStrip.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 20);
            this.lblStatus.Text = "Ready";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1774, 882);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "AdminForm";
            this.Text = "NetMail - Admin Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelUsersButtons.ResumeLayout(false);
            this.tabEmailSender.ResumeLayout(false);
            this.tabEmailSender.PerformLayout();
            this.grpMailContent.ResumeLayout(false);
            this.grpMailContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalMinutes)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabEmailSender;

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVerified;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsAdmin;

        private System.Windows.Forms.Panel panelUsersButtons;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnToggleVerification;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.GroupBox grpMailContent;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.TextBox txtCc;
        private System.Windows.Forms.Label lblBcc;
        private System.Windows.Forms.TextBox txtBcc;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.TextBox txtAttachment;
        private System.Windows.Forms.Button btnBrowse;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpScheduledTime;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown nudIntervalMinutes;
        private System.Windows.Forms.Button btnStartSchedule;
        private System.Windows.Forms.Button btnStopSchedule;
        private System.Windows.Forms.Label lblScheduleStatus;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstInvalid;
        private System.Windows.Forms.Button btnRemoveInvalid;

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnResendSelected;

        private System.Windows.Forms.Label lblSmtp;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnLoadFromExcel;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}
