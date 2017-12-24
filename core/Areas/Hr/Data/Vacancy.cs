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
        public string Ref { get; set; }

        [Column(TypeName = "text")]
        public string Annotation { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public Vacancy() {}

        public Vacancy(string name, string href, string annotation, string text)
            :this(0, name, href, annotation, text) {}

        public Vacancy(int id, string name, string href, string annotation, string text)
        {
            Id = id;
            Name = name;
            Ref = href;
            Annotation = annotation;
            Text = text;
        }
    }
}
