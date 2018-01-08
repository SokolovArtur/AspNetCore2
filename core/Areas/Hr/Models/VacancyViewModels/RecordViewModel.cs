using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochka.Attributes;
using Tochka.Models;

namespace Tochka.Areas.Hr.Models.VacancyViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
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

        [StringLength(4000)]
        public string Annotation { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public IEnumerable<SelectListItem> CitiesForSelection { get; set; }

        [NotNull]
        public IEnumerable<int> ListCitiesIds { get; set; }
    }
}
