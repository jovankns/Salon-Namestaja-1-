using SalonNamestaja.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonNamestaja.Controllers
{
    public class CartController : Controller
    {
        public static List<Cart> cartList = new List<Cart>(); 
        public static string Cartlocation = @"~/Cart.txt";
        static string location = @"~/Namestaj.txt";

        public ActionResult Index()
        {
            double ukupnaCena = 0;
            List<Cart> myList2 = new List<Cart>();
            using (StreamReader sr = new StreamReader(Server.MapPath(Cartlocation)))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] array = line.Split(',');
                        Cart cr = new Cart();
                        cr.Sifra = int.Parse(array[0]);
                        cr.Naziv = array[1];
                        cr.Boja = array[2];
                        cr.Cena = double.Parse(array[3]);
                        cr.Kolicina = int.Parse(array[4]);

                        ukupnaCena += cr.Cena * cr.Kolicina;
                        myList2.Add(cr);
                    }
                }
            }
            cartList = myList2;
            ViewBag.UkupnaCena = ukupnaCena;
            return View(cartList);
        }
        public void Delete(int id)
        {
            Cart deleteCart = new Cart();
            int counter = 0;
            if (cartList.Exists(x => x.Sifra == id))
            {
                deleteCart = cartList.Where(x => x.Sifra == id).Select(x => x).FirstOrDefault();
                cartList.Remove(deleteCart);
            }
            counter = deleteCart.Kolicina;
            using (StreamWriter sw = new StreamWriter(Server.MapPath(Cartlocation)))
            {
                foreach (var item in cartList)
                {
                    sw.WriteLine(item.Sifra + "," + item.Naziv + "," + item.Boja + "," + item.Cena + "," + item.Kolicina);
                }
            }
            List<Furniture> myList3 = new List<Furniture>();
            using (StreamReader sr = new StreamReader(Server.MapPath(location)))
            {
                string line = "";
                int count = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (count > 0)
                    {
                        string[] array = line.Split(',');

                        Furniture km = new Furniture();
                        km.name = array[1];
                        km.color = array[2];
                        km.country = array[3];
                        km.nameOfTheManufacturer = array[4];
                        km.price = array[5];
                        km.quantity = array[6];
                        km.CategoryName = array[7];
                        km.year = array[8];
                        km.salon = array[9];
                        myList3.Add(km);

                    }
                    count++;
                }
            }
            for (int i = 0; i < myList3.Count; i++)
            {
                if (myList3[i].id == id)
                    myList3[i].quantity += counter;
            }
            WriteTXT.WriteFurniture(myList3);
            TempData["Success"] = "Uspesno ste obrisali iz korpe.";
        }
        public ActionResult DeleteCart(int id)
        {
            Delete(id);
            return RedirectToAction("Index");          
            
        }

        public ActionResult FlushCart()
        {
            for (int i = 0; i < cartList.Count;i++)
            {
                Delete(cartList[i].Sifra);
            }
                using (StreamWriter cleaner = new StreamWriter(Server.MapPath(Cartlocation)))
                {
                    cleaner.Flush();
                }
            return RedirectToAction("Index");
        }

        public ActionResult Racun()
        {
            double ukupnaCena = 0;
            List<Cart> myList2 = new List<Cart>();
            using (StreamReader sr = new StreamReader(Server.MapPath(Cartlocation)))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] array = line.Split(',');
                    Cart cr = new Cart();
                    cr.Sifra = int.Parse(array[0]);
                    cr.Naziv = array[1];
                    cr.Boja = array[2];
                    cr.Cena = double.Parse(array[3]);
                    cr.Kolicina = int.Parse(array[4]);

                    ukupnaCena += cr.Cena * cr.Kolicina;
                    myList2.Add(cr);
                }
            }
            cartList = myList2;
            ViewBag.UkupnaCena = ukupnaCena;
            using(StreamWriter clear = new StreamWriter(Server.MapPath(Cartlocation)))
            {
                clear.Flush();
            }
            return View(cartList);
        }

        //public ActionResult GeneratePDF()
        //{
        //    return new ActionAsPdf("Racun");
        //}
    }
}