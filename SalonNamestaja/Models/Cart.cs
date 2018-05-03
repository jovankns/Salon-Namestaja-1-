using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class Cart
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public string Boja { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public double UkupnaCena { get; set; }
    }
}