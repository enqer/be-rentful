using Rentful.Domain.Entities;

namespace Rentful.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailFromUserAsync(User sender, string recepient, string subject, string message);
    }
}
