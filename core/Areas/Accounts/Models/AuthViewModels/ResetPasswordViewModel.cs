using System.ComponentModel.DataAnnotations;

namespace Tochka.Areas.Accounts.Models.AuthViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(
            @"\w{6,100}",
            ErrorMessage = "{0} must have at least one lowercase ('a'-'z'), " +
                           "one uppercase ('A'-'Z') and " +
                           "one digit ('0'-'9').")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
