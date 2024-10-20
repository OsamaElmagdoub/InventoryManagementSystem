using MailKit.Net.Smtp;
using MimeKit;

namespace InventoryManagementSystem.Helpers
{
    public static class EmailService
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("SENDER_NAME"), Environment.GetEnvironmentVariable("EMAIL_ADDRESS")));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(Environment.GetEnvironmentVariable("EMAIL_HOST"), 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate(Environment.GetEnvironmentVariable("EMAIL_ADDRESS"), Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
                    client.Send(message);
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {

                    client.Disconnect(true);
                }

            }
        }
    }
}
