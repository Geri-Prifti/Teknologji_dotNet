using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt_Teknologji_dotNet.Models
{
    [Table("Type")]
    public class Tipi
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public string Emri { get; set; }
        [Required]
        [Display(Name = "Ikona")]
        public string Ikona { get; set; }

        public static implicit operator Tipi(string v)
        {
            throw new NotImplementedException();
        }
    }
}