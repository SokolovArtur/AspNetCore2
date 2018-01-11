using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tochka.Areas.Accounts.Extensions;

namespace Tochka.Areas.Accounts.Models.UserViewModels
{
    public class RecordViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        [Remote(areaName: "Accounts", controller: "User", action: "RemoteUserNameIsUnique", AdditionalFields = "Id")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [Remote(areaName: "Accounts", controller: "User", action: "RemoteEmailIsUnique", AdditionalFields = "Id")]
        [EmailAddress]
        public string Email { get; set; }

        public string Password
        {
            get
            {
                return PasswordGeneratorExtension.Generate(new PasswordOptions
                {
                    RequiredLength = 8,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = true
                });
            }
        }
    }
}
