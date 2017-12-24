using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tochka.Areas.Geodata.Data;

namespace Tochka.Areas.Hr.Data
{
    [Table("VacanciesCities")]
    public class VacancyCity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VacancyId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Column(TypeName = "timestamp")]
        [Timestamp]
        [Required]
        public string ActiveFrom { get; set; }

        [Column(TypeName = "timestamp")]
        [Timestamp]
        public string ActiveTo { get; set; }

        public Vacancy Vacancy { get; set; }

        public City City { get; set; }
    }
}
