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
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Naslov { get; set; }

        [Column("godiste")]
        [Display(Name = "Godište automobila")]
        [Range(1900, 2022, ErrorMessage = "Godište mora biti između {1} i {2}")]
        public float Godiste { get; set; }

        [Column("stanje")]
        [Display(Name = "Stanje automobila")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Stanje { get; set; }

        [Column("cijena")]
        [Display(Name = "Cijena automobila")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public int Cijena { get; set; }

        [Column("opis")]
        [Display(Name = "Opis oglasa")]
        public string Opis { get; set; }

        [Column("id_korisnika")]
        [Display(Name = "ID korisnika")]
        public int ID_Korisnika { get; set; }

        [Column("username")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Column("datum")]
        [Display(Name = "Datum objave")]
        public string Datum { get; set; }

    }
}