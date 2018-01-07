using System.ComponentModel.DataAnnotations;

namespace Tochka.Areas.Accounts.Models.UserViewModels
{
    public class RecordViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public string Password
        {
            get
            {
                return "Pa$$w0rd";
            }
        }
    }
}
