using System.ComponentModel.DataAnnotations;

namespace Tochka.Areas.Accounts.Models.AuthViewModels
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }

        public string Code { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
