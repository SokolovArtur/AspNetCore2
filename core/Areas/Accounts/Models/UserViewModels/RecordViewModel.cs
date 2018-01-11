using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

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
                return "GUID" + Guid.NewGuid().ToString("N");
            }
        }
    }
}
