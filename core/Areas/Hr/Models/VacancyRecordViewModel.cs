using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochka.Models;

namespace Tochka.Areas.Hr.Models
{
    public class VacancyRecordViewModel
    {
        public VacancyRecordViewModel()
        {
            // ...
        }

        public VacancyRecordViewModel(IEnumerable<SelectListItem> citiesList)
        {
            CitiesList = citiesList;
        }

        public VacancyRecordViewModel(int id, string name, IEnumerable<SelectListItem> citiesList, int? city, string annotation, string text)
        {
            Id = id;
            CitiesList = citiesList;
            City = city;
            Name = name;
            Annotation = annotation;
            Text = text;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> CitiesList { get; set; }

        [Required]
        public int? City { get; set; }

        [StringLength(255)]
        public string Ref {
            get
            {
                string href = Transliteration.CyrillicToLatin(Name);
                href.Replace("`", "").Replace("'", "").Replace(" ", "");
                return WebUtility.UrlEncode(href);
            }
        }

        [StringLength(4000)]
        public string Annotation { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }
    }
}
