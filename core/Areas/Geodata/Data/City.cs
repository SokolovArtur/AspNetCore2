﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochka.Areas.Geodata.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required]
        public string LatinName { get; set; }

        [DefaultValue("false")]
        public bool IsRepresentation { get; set; }

        public City() { }
    }
}
