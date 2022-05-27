﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web_oglasnik.Models
{
    [Table("ovlasti")]
    public class Ovlast
    {
        public string Sifra { get; set; }

        public string Naziv { get; set; }
    }
}