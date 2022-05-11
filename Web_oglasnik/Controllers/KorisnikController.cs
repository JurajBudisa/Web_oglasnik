using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_oglasnik.Models;

namespace Web_oglasnik.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                using(BazaDbContext db = new BazaDbContext())
                {
                    db.PopisKorisnika.Add(korisnik);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = korisnik.Ime + " " + korisnik.Prezime + " uspješno registriran.";
            }
            return View("~/Views/Oglas/Index.cshtml");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Korisnik korisnik)
        {
            using(BazaDbContext db = new BazaDbContext())
            {
                var usr = db.PopisKorisnika.Single(u => u.Username == korisnik.Username && u.Password == korisnik.Password);
                if (usr != null)
                {
                    Session["ID"] = usr.ID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return View("~/Views/Oglas/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError("", "korisničko ime ili lozinka je pogrešna");
                }
            }
            return View();
        }

        public ActionResult Odjava()
        {
            Session.Clear();
            return View("~/Views/Oglas/Index.cshtml");
        }
    }
}