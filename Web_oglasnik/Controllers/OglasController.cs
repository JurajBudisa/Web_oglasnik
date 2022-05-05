﻿using Web_oglasnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web_oglasnik.Controllers
{
    public class OglasController : Controller
    {
        BazaDbContext bazaPOdataka = new BazaDbContext();
        // GET: Oglas
        public ActionResult Index()
        {
            ViewBag.Title = "Web oglasnik početna";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }

        public ActionResult Popis(string naziv, string stanje)
        {
            var oglasi = bazaPOdataka.PopisOglasa.ToList();

            if (!String.IsNullOrWhiteSpace(naziv))
                oglasi = oglasi.Where(x => x.Naslov.ToUpper().Contains(naziv.ToUpper())).ToList();
            if (!String.IsNullOrWhiteSpace(stanje))
                oglasi = oglasi.Where(x => x.Stanje == stanje).ToList();
            return View(oglasi);
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Oglas oglas = bazaPOdataka.PopisOglasa.FirstOrDefault(x => x.ID == id);

            if (oglas == null)
            {
                return HttpNotFound();
            }
            return View(oglas);
        }

        public ActionResult Azuriraj(int? id)
        {
            Oglas oglas = null;
            if (!id.HasValue)
            {
                oglas = new Oglas();
                ViewBag.Title = "Kreiranje oglasa";
                ViewBag.Novi = true;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                oglas = bazaPOdataka.PopisOglasa
                    .FirstOrDefault(s => s.ID == id);

                if (oglas == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Title = "Ažuriranje postojećeg oglasa";
                ViewBag.Novi = false;
            }
            return View(oglas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Oglas s)
        {
            if (ModelState.IsValid)
            {
                if (s.ID != 0)
                    bazaPOdataka.Entry(s).State = System.Data.Entity.EntityState.Modified;
                else
                    bazaPOdataka.PopisOglasa.Add(s);
                bazaPOdataka.SaveChanges();
                return RedirectToAction("Popis");
            }

            if (s.ID != 0)
            {
                ViewBag.Title = "Ažuriranje oglasa";
                ViewBag.Novi = false;
            }
            else
            {
                ViewBag.Title = "Kreiranje novog oglasa";
                ViewBag.Novi = true;
            }
            return View(s);
        }

        public ActionResult Brisi(int? id)
        {
            if (id == null)
                return RedirectToAction("Popis");
            Oglas s = bazaPOdataka.PopisOglasa.FirstOrDefault(x => x.ID == id);
            if (s == null)
                return HttpNotFound();
            ViewBag.Title = "Potvrda brisanja oglasa";
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Oglas s = bazaPOdataka.PopisOglasa
                .FirstOrDefault(x => x.ID == id);
            if (s == null)
                return HttpNotFound();
            bazaPOdataka.PopisOglasa.Remove(s);
            bazaPOdataka.SaveChanges();
            return View("BrisiStatus");
        }

        public ActionResult Prijava()
        {
            ViewBag.Title = "Prijava";
            return View("Prijava");
        }

        public ActionResult Registracija()
        {
            ViewBag.Title = "Registracija";
            return View("Registracija");
        }
    }
}