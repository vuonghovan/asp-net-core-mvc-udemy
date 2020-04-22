using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string nameTo, string emailTo, string subject, BodyBuilder body);
        Task<bool> SendEmailAsync1(string emailTo, string subject, string body);
    }
}
