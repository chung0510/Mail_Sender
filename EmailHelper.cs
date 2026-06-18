using System;
using System.Net;
using System.Net.Mail;

namespace NetMail
{
    public static class EmailHelper
    {
        // Gửi email xác minh với mã xác minh
        // Phải cung cấp thông tin SMTP server
        public static bool SendVerificationEmail(string smtpServer, int port, bool enableSsl, string fromEmail, string fromPassword, string toEmail, string verificationCode, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var client = new SmtpClient(smtpServer))
                {
                    client.Port = port;
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(fromEmail, fromPassword);

                    var msg = new MailMessage();
                    msg.From = new MailAddress(fromEmail);
                    msg.To.Add(toEmail);
                    msg.Subject = "Your NetMail verification code";
                    msg.Body = $"Your verification code: {verificationCode}\n\nEnter this code into the app to verify your account.";
                    client.Send(msg);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
