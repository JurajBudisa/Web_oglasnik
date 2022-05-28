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

        [Display(Name = "Marka")]
        [Column("marka")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public Marke? Marka { get; set; }

        [Column("godiste")]
        [Display(Name = "Godište automobila")]
        [Range(1900, 2022, ErrorMessage = "{0} mora biti između {1} i {2}")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "{0} mora biti broj")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Godiste { get; set; }

        [Column("stanje")]
        [Display(Name = "Stanje automobila")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Stanje { get; set; }

        [Column("cijena")]
        [Display(Name = "Cijena automobila")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Required(ErrorMessage = "{0} je obavezna")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "{0} mora biti broj")]
        public string Cijena { get; set; }

        [Column("opis")]
        [Display(Name = "Opis oglasa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Opis { get; set; }

        [Column("korisnicko_ime")]
        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string KorisnickoIme { get; set; }

        [Column("datum")]
        [Display(Name = "Datum objave")]
        public string Datum { get; set; }

        [Column("slika")]
        [Display(Name = "Slika")]
        public string Slika { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}