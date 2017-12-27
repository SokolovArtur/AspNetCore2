using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochka.Areas.Hr.Data
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required]
        public string LatinName { get; set; }

        [Column(TypeName = "text")]
        public string Annotation { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public ICollection<VacancyCity> VacanciesCities { get; set; }

        public Vacancy()
        {
            VacanciesCities = new List<VacancyCity>();
        }
    }
}
