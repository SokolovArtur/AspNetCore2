using System.Threading.Tasks;

namespace Tochka.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
