using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NetMail
{
    public partial class LoginForm : Form
    {
        private bool isRegisterMode = false;

        public LoginForm()
        {
            InitializeComponent();
            DatabaseHelper.EnsureDatabase();
            SwitchMode(false);
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
            isRegisterMode = !isRegisterMode;
            SwitchMode(isRegisterMode);
        }

        private void SwitchMode(bool registerMode)
        {
            lblTitle.Text = registerMode ? "Register" : "Login";
            btnSubmit.Text = registerMode ? "Register" : "Login";
            btnToggle.Text = registerMode ? "Already have an account? Login" : "Don’t have an account? Register";
            lblVerify.Visible = false;
            txtVerifyCode.Visible = false;
            btnVerify.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!ValidateInputs(email, password)) return;

            if (isRegisterMode)
                HandleRegister(email, password);
            else
                HandleLogin(email, password);
        }

        private bool ValidateInputs(string email, string password)
        {
            // --- Email validation ---
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email cannot be empty.");
                return false;
            }
            // Simple regex for email format
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return false;
            }

            // --- Password constraints ---
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.");
                return false;
            }
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return false;
            }
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBox.Show("Password must contain at least one uppercase letter.");
                return false;
            }
            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                MessageBox.Show("Password must contain at least one lowercase letter.");
                return false;
            }
            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Password must contain at least one digit.");
                return false;
            }

            return true;
        }

        private void HandleRegister(string email, string password)
        {
            var existing = DatabaseHelper.GetUserByEmail(email);
            if (existing != null)
            {
                MessageBox.Show("This email is already registered.");
                return;
            }

            string stored = HashHelper.CreateStoredPassword(password);
            string code = new Random().Next(100000, 999999).ToString();

            bool created = DatabaseHelper.CreateUser(email, stored, code);
            if (!created)
            {
                MessageBox.Show("Failed to create user (possibly duplicate email).");
                return;
            }

            string error;
            bool ok = EmailHelper.SendVerificationEmail(
                AppConfig.SmtpServer,
                AppConfig.Port,
                AppConfig.UseSsl,
                AppConfig.FromEmail,
                AppConfig.FromPassword,
                email,
                code,
                out error);

            if (!ok)
            {
                MessageBox.Show("Failed to send verification email:\n" + error);
                return;
            }

            MessageBox.Show("Verification code sent! Check your email.");
            lblVerify.Visible = true;
            txtVerifyCode.Visible = true;
            btnVerify.Visible = true;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string code = txtVerifyCode.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Enter the code you received by email.");
                return;
            }

            bool ok = DatabaseHelper.VerifyUserByCode(email, code);
            if (ok)
            {
                MessageBox.Show("Email verified! You can now log in.");
                isRegisterMode = false; 
                SwitchMode(false);
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Incorrect verification code.");
            }
        }

        private void HandleLogin(string email, string password)
        {
            var user = DatabaseHelper.GetUserByEmail(email);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            if (!user.IsVerified)
            {
                MessageBox.Show("Email not verified yet. Please verify first.");
                return;
            }

            bool valid = HashHelper.ValidatePassword(password, user.PasswordHashWithSalt);
            if (!valid)
            {
                MessageBox.Show("Wrong password.");
                return;
            }

            Hide();
            if (email == "admin@netmail.com")
            {
                var admin = new AdminForm();
                admin.FormClosed += (s, args) => this.Close();
                admin.Show();
                return;
            }
            else
            {
                var main = new User();
                main.FormClosed += (s, args) => this.Close();
                main.Show();
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}
