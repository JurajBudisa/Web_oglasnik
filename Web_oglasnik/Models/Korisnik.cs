﻿using System;
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
        public string Ime { get; set; }

        [Column("prezime")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Column("email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        public string Email { get; set; }

        [Column("mobitel")]
        [Display(Name = "Broj mobitela")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Neispravan broj mobitela")]
        public string Mobitel { get; set; }

        [Column("username")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Column("lozinka")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}