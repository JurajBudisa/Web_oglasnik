using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web_oglasnik.Models
{
    [Table("oglasi")]
    public class Oglas
    {

        [Key]
        [Display(Name = "ID oglasa")]
        public int ID { get; set; }

        [Display(Name = "Naslov")]
        public string Naslov { get; set; }

        [Column("godiste")]
        [Display(Name = "Godište automobila")]
        public float Godiste { get; set; }

        [Column("stanje")]
        [Display(Name = "Stanje automobila")]
        public string Stanje { get; set; }

        [Column("cijena")]
        [Display(Name = "cijena automobila")]
        public string Cijena { get; set; }

    }
}