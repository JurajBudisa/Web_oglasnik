using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_oglasnik.Controllers;
using Web_oglasnik.Models;

using System.Net;
using System.Web;

namespace Web_oglasnik_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private int Zbroji(int a, int b)
        {
            return a + b;
        }



        [TestMethod]
        public void TestZbrajanje()
        {
            //Arrange
            int x = 10;
            int y = 5;

            //Act
            int zbroj = Zbroji(x, y);

            //Assert
            Assert.AreEqual(15, zbroj);
        }

        [TestMethod]
        public void TestOglasiIndexTitle()
        {
            //Arrange
            OglasController controller = new OglasController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Web oglasnik početna", result.ViewBag.Title);
        }

        [TestMethod]
        public void Oglas_KreiranjeBrisanje()
        {
            //Arrange
            BazaDbContext db = new BazaDbContext();

            Oglas oglas = new Oglas
            {
                ID = 0,
                Naslov = "Oglas",
                Marka = (Marke?)2,
                Godiste = "2000",
                Stanje = "N",
                Opis = "Opis",
                Cijena = "5000000",
                KorisnickoIme = "Korisnik",
                Datum = "5/29/2022",
                Slika = "~/Images/Kolokvij mat225228090.png"

            };

            //Act
            db.PopisOglasa.Add(oglas);
            db.SaveChanges();

            db.PopisOglasa.Remove(oglas);
            int obrisano = db.SaveChanges();

            //Assert
            Assert.AreEqual(obrisano, 1);
        }

        [TestMethod]
        public void Oglas_NaslovNotNull()
        {
            //Arrabge
            Oglas testOglas = new Oglas
            {
                Naslov = null
            };

            //Act
            var ctx = new ValidationContext(testOglas) { MemberName = nameof(testOglas.Naslov) };
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateProperty(testOglas.Naslov, ctx, result);

            //Assert
            Assert.IsFalse(valid);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("Naslov je obavezan", result[0].ErrorMessage);
        }
    }
}
