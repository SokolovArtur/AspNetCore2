using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Tochka.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(
                email,
                "Reset Password",
                $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
        
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
