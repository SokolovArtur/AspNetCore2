using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tochka.Models;

namespace Tochka.Areas.Accounts.Models.UserViewModels
{
    public class LockoutViewModel
    {
        public string Id { get; set; }
        
        [Required]
        public int? TermId { get; set; }
        
        private enum Term
        {
            Non = 1,
            Hour = 2,
            Day = 3,
            Week = 4,
            Month = 5,
            Quarter = 6,
            Year = 7,
            Forever = 8
        }

        public IEnumerable<RadioListItem> LockoutTerm
        {
            get
            {
                return new List<RadioListItem>
                {
                    new RadioListItem { Value = (int) Term.Non, Text = "Не блокировать" },
                    new RadioListItem { Value = (int) Term.Hour, Text = "Час" },
                    new RadioListItem { Value = (int) Term.Day, Text = "День" },
                    new RadioListItem { Value = (int) Term.Week, Text = "Неделя" },
                    new RadioListItem { Value = (int) Term.Month, Text = "Месяц" },
                    new RadioListItem { Value = (int) Term.Quarter, Text = "Квартал" },
                    new RadioListItem { Value = (int) Term.Year, Text = "Год" },
                    new RadioListItem { Value = (int) Term.Forever, Text = "До 2038 года" }
                };
            }
        }
        
        public DateTimeOffset? LockoutEnd
        {
            get
            {
                DateTimeOffset? result;
                DateTimeOffset currentTime = DateTimeOffset.Now;
                switch (TermId)
                {
                    case (int) Term.Non:
                        result = null;
                        break;
                    case (int) Term.Hour:
                        result = currentTime.AddHours(1);
                        break;
                    case (int) Term.Day:
                        result = currentTime.AddDays(1);
                        break;
                    case (int) Term.Week:
                        result = currentTime.AddDays(7);
                        break;
                    case (int) Term.Month:
                        result = currentTime.AddMonths(1);
                        break;
                    case (int) Term.Quarter:
                        result = currentTime.AddMonths(3);
                        break;
                    case (int) Term.Year:
                        result = currentTime.AddYears(1);
                        break;
                    case (int) Term.Forever:
                        result = new DateTime(2038, 1, 1);
                        break;
                    default:
                        result = null;
                        break;
                }
                return result;
            }
            set
            {
                DateTimeOffset currentTime = DateTimeOffset.Now;
                
                if (value == null || value < currentTime)
                {
                    TermId = (int) Term.Non;
                    return;
                }

                var differenceTime = value - currentTime;
                if (differenceTime > TimeSpan.FromDays(366))
                {
                    TermId = (int) Term.Forever;
                    return;
                }
                if (differenceTime > TimeSpan.FromDays(92))
                {
                    TermId = (int) Term.Year;
                    return;
                }
                if (differenceTime > TimeSpan.FromDays(31))
                {
                    TermId = (int) Term.Quarter;
                    return;
                }
                if (differenceTime > TimeSpan.FromDays(7))
                {
                    TermId = (int) Term.Month;
                    return;
                }
                if (differenceTime > TimeSpan.FromDays(1))
                {
                    TermId = (int) Term.Week;
                    return;
                }
                if (differenceTime > TimeSpan.FromHours(1))
                {
                    TermId = (int) Term.Day;
                    return;
                }
                else
                {
                    TermId = (int) Term.Hour;
                    return;
                }
            }
        }
    }
}