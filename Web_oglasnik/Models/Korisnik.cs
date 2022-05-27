using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Web_oglasnik.Models
{
    [Table("korisnici")]
    public class Korisnik
    {

        [Key]
        [Display(Name = "ID korisnika")]
        public int ID { get; set; }

        [Column("ime")]
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Ime { get; set; }

        [Column("prezime")]
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Prezime { get; set; }

        public string PrezimeIme
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }

        [Column("ovlast")]
        [Display(Name = "Ovlast")]
        [ForeignKey("Ovlast")]
        public string SifraOvlasti { get; set; }

        public virtual Ovlast Ovlast { get; set; }

        [Column("email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Email { get; set; }

        [Column("mobitel")]
        [Display(Name = "Broj mobitela")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Neispravan broj mobitela")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Mobitel { get; set; }

        [Column("username")]
        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Username { get; set; }

        [Column("lozinka")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} je obavezna")]
        [NotMapped]
        public string Password { get; set; }

        [Display(Name = "Ponovljena lozinka")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Lozinke moraju biti jednake")]
        public string Password2 { get; set; }
    }
}