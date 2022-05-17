using Web_oglasnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web_oglasnik.Controllers
{
    public class KorisnikController : Controller
    {
        BazaDbContext bazaPOdataka = new BazaDbContext();
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
                var usr = db.PopisKorisnika.FirstOrDefault(u => u.Username == korisnik.Username && u.Password == korisnik.Password);
                if (usr != null)
                {
                    Session["ID"] = usr.ID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return View("~/Views/Oglas/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError("", "Pogrešno korisničko ime i/ili lozinka");
                }
            }
            return View();
        }

        public ActionResult Odjava()
        {
            Session.Clear();
            return View("~/Views/Oglas/Index.cshtml");
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Korisnik korisnik = bazaPOdataka.PopisKorisnika.FirstOrDefault(x => x.ID == id);

            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        public ActionResult Azuriraj(int? id)
        {
            Korisnik korisnik = null;

                korisnik = bazaPOdataka.PopisKorisnika
                    .FirstOrDefault(s => s.ID == id);

                if (korisnik == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Title = "Ažuriranje korisnika";
                ViewBag.Novi = false;

            return View(korisnik);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                if (korisnik.ID != 0)
                    bazaPOdataka.Entry(korisnik).State = System.Data.Entity.EntityState.Modified;
                else
                    bazaPOdataka.PopisKorisnika.Add(korisnik);
                bazaPOdataka.SaveChanges();
                return RedirectToAction("Index", "Oglas");
            }

                ViewBag.Title = "Ažuriranje korisnika";

            return View(korisnik);
        }
    }
}