using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_oglasnik.Models
{
    public class OglasiDB
    {
        private static List<Oglas> lista = new List<Oglas>();
        private static bool listaInicijalizirana = false;

        public OglasiDB()
        {

            if (listaInicijalizirana == false)
            {

                listaInicijalizirana = true;
                lista.Add(new Oglas()
                {
                    ID = 1,
                    Naslov = "auto",
                    Godiste = "1991",
                    Stanje = "R"
                });
            }
        }

        public List<Oglas> VratiListu()
        {
            return lista;
        }

        public void AzurirajOglas(Oglas oglas)
        {
            int oglasIndex = lista.FindIndex(s => s.Naslov == oglas.Naslov);
            lista[oglasIndex] = oglas;
        }
    }
}