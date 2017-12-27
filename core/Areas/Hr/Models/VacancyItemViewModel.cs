using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tochka.Areas.Hr.Models
{
    public class VacancyItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название вакансии")]
        public string Name { get; set; }

        [Display(Name = "Аннотация")]
        public string Annotation { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Город(а)")]
        public IEnumerable<string> NamesOfCities { get; set; }

        public VacancyItemViewModel() { }
    }
}
