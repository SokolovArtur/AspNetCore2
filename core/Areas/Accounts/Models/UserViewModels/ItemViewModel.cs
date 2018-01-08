using System;
using System.ComponentModel.DataAnnotations;

namespace Tochka.Areas.Accounts.Models.UserViewModels
{
    public class ItemViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        private DateTimeOffset? _lockoutEnd;

        [DisplayFormat(DataFormatString = "{0:HH:mm dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? LockoutEnd
        {
            get { return _lockoutEnd; }
            set { _lockoutEnd = (value > DateTimeOffset.Now) ? value : null; }
        }
    }
}
