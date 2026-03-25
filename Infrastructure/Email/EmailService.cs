using edu_connect_backend.WebAPI.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace edu_connect_backend.Infrastructure.Email
{
    public class EmailService
    {
        private readonly SmtpSettings smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string toName, string subject, string textBody, string htmlBody = "")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = textBody;

            if (!string.IsNullOrEmpty(htmlBody))
            {
                builder.HtmlBody = htmlBody;
            }

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            var socketOptions = smtpSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;

            try
            {
                await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, socketOptions);

                if (!string.IsNullOrEmpty(smtpSettings.Username))
                {
                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
                }

                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}