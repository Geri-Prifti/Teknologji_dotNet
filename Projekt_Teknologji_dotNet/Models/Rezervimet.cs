using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt_Teknologji_dotNet.Models
{
    [Table("Reservation")]
    public class Rezervimet
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Date Rezervimi")]
        public DateTime Date_Rezervimi { get; set; }

        [Required]
        [Display(Name = "Date Kthimi")]
        public DateTime Date_kthimi { get; set; }

        [Required]
        public decimal Pagesa_totale { get; set; }
    }
}
