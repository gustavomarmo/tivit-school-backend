using edu_connect_backend.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace edu_connect_backend.Service
{
    public class EmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string toName, string subject, string textBody, string htmlBody = "")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
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

            // Conexão segura
            // Use SecureSocketOptions.StartTls para portas 587 ou 2525
            // Use SecureSocketOptions.SslOnConnect para porta 465
            var socketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;

            try
            {
                await client.ConnectAsync(_settings.Server, _settings.Port, socketOptions);

                if (!string.IsNullOrEmpty(_settings.Username))
                {
                    await client.AuthenticateAsync(_settings.Username, _settings.Password);
                }

                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Em produção, logar o erro corretamente
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