using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochka.Attributes;
using Tochka.Models;

namespace Tochka.Areas.Hr.Models
{
    public class VacancyRecordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        [StringLength(255, ErrorMessage = "Максимальная длина строки 255 символов.")]
        [Display(Name = "Название вакансии")]
        public string Name { get; set; }

        [StringLength(255)]
        public string LatinName
        {
            get
            {
                string href = Transliteration.CyrillicToLatin(Name);
                return href.Replace("`", "").Replace("'", "");
            }
        }

        [StringLength(4000, ErrorMessage = "Максимальная длина строки 4000 символов.")]
        [Display(Name = "Аннотация")]
        public string Annotation { get; set; }

        [StringLength(4000, ErrorMessage = "Максимальная длина строки 4000 символов.")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        public IEnumerable<SelectListItem> CitiesForSelection { get; set; }

        [NotNull(ErrorMessage = "Поле обязательно для заполнения.")]
        [Display(Name = "Город(а)")]
        public IEnumerable<int> ListCitiesIds { get; set; }

        public VacancyRecordViewModel() { }
    }
}
