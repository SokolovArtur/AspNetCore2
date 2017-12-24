using System.ComponentModel.DataAnnotations;
using System.Net;
using Tochka.Models;

namespace Tochka.Areas.Hr.Models
{
    public class VacancyRecordViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Ref {
            get
            {
                if (Name == null)
                {
                    return null;
                }

                string href = Transliteration.CyrillicToLatin(Name);
                href = href.Replace("`", "").Replace("'", "").Replace(" ", "-").ToLower();
                href = WebUtility.UrlEncode(href);
                return href;
            }
        }

        [StringLength(4000)]
        public string Annotation { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public VacancyRecordViewModel()
        {
            // ...
        }

        public VacancyRecordViewModel(int id, string name, string annotation, string text)
        {
            Id = id;
            Name = name;
            Annotation = annotation;
            Text = text;
        }
    }
}
