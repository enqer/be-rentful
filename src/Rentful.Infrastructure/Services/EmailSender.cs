using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Options;

namespace Rentful.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings mailSettings;

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Rentful", mailSettings.Login));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(mailSettings.Smtp, mailSettings.Port, true);
            await client.AuthenticateAsync(mailSettings.Login, mailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
