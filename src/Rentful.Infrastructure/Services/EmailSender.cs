using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
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

        public async Task SendEmailFromUserAsync(User sender, string recepient, string subject, string message)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress($"{sender.FirstName} {sender.LastName}", mailSettings.Login));
            msg.To.Add(new MailboxAddress("", recepient));
            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(mailSettings.Smtp, mailSettings.Port, true);
            await client.AuthenticateAsync(mailSettings.Login, mailSettings.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
