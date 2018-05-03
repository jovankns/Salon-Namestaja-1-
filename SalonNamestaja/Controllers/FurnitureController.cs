using SalonNamestaja.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonNamestaja.Controllers
{
    public class FurnitureController : Controller
    {
        // GET: Furniture INDEX
        public ActionResult Index()
        {
            List<Furniture> list = ReadTXT.ReadTXTFileAndFillListFurniture();
            ViewBag.CategoryName = new SelectList(ReadTXT.ReadTXTFileAndFillListFC(), "CategoryName", "CategoryName");
            return View(list);
        }
        [HttpPost] // SEARCH
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(string searchTerm, string CategoryName, string searchName,string searchColor , string searchCountry
            , string searchManufacturer, string searchPrice, string searchQuantity, string searchYear)
        {
            ViewBag.CategoryName = new SelectList(ReadTXT.ReadTXTFileAndFillListFC(), "CategoryName", "CategoryName");
            List<Furniture> list;
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            list = (from x in listAllFurniture select x).ToList();
            int price = 0, quantity = 0, year = 0;
            if (!String.IsNullOrEmpty(searchPrice) && !Int32.TryParse(searchPrice, out price))
            {
                ModelState.AddModelError("", "Wrong type for Price.");
            } 
            if (!String.IsNullOrEmpty(searchQuantity) && !Int32.TryParse(searchQuantity, out quantity))
            {
                ModelState.AddModelError("", "Wrong type for Quantity.");
            }
            if (!String.IsNullOrEmpty(searchYear) && !Int32.TryParse(searchYear, out year))
            {
                ModelState.AddModelError("", "Wrong type for Year.");
            }
            if (!String.IsNullOrEmpty(CategoryName) || !String.IsNullOrEmpty(searchName) || !String.IsNullOrEmpty(searchColor) ||
                !String.IsNullOrEmpty(searchCountry) || !String.IsNullOrEmpty(searchManufacturer) || 
                !String.IsNullOrEmpty(searchPrice) || !String.IsNullOrEmpty(searchQuantity) || !String.IsNullOrEmpty(searchYear))
            {
                    list = list.Where(x =>
                    {
                        var categoryMatches = x.CategoryName != null && x.CategoryName.ToLower().Contains(CategoryName.ToLower());
                        var nameMatches = searchName != null && x.name.ToLower().Contains(searchName.ToLower());
                        var colorMatches = searchColor != null && x.color.ToLower().Contains(searchColor.ToLower());
                        var countryMatches = searchCountry != null && x.country.ToLower().Contains(searchCountry.ToLower());
                        var nameManufactureMatches = searchManufacturer != null && x.nameOfTheManufacturer.ToLower().Contains(searchManufacturer.ToLower());
                        var priceMatches = searchPrice != null && x.price.ToLower().Contains(searchPrice.ToLower());
                        var quantityMatches = searchQuantity != null && x.quantity.ToLower().Contains(searchQuantity.ToLower());
                        var yearmatches = searchYear != null && x.year.ToLower().Contains(searchYear.ToLower());
                        return categoryMatches && nameMatches && colorMatches && countryMatches && nameManufactureMatches &&
                            priceMatches && quantityMatches && yearmatches;
                    }).ToList();
                    return View(list);
            }
            else
            {
                return View(list);
            }
        }
        [HttpPost] // SEARCH
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult Index2(string Prefix)
        {

            List<FurnitureCategory> list = ReadTXT.ReadTXTFileAndFillListFC().ToList();
            var empName = (from e in list where e.CategoryName.ToLower().Contains(Prefix.ToLower()) select new { e.CategoryName });
            return Json(empName, JsonRequestBehavior.AllowGet);
        }

        //CREATE
        public ActionResult Create()
        {
            ViewBag.Names = ReadTXT.ReadTXTFileAndFillListFC();
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id, name, color, country, nameOfTheManufacturer, price, quantity, CategoryName, year, salon, ImageSource")]
            Furniture furniture)
        {
            furniture.ImageSource = "../../Images/" + furniture.ImageSource;
            AllMethods.AddFurniture(furniture);
            return RedirectToAction("Index");
        }

        //EDIT
        public ActionResult Edit(int id)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture piece = (from p in listAllFurniture where p.id == id select p).First();
            Session["FurnitureCode"] = piece.id;
            //Session["PictureType"] = piece.ImageSource;
            ViewBag.Names = ReadTXT.ReadTXTFileAndFillListFC();
            return View(piece);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, color, country, nameOfTheManufacturer, price, quantity, CategoryName, year, salon, ImageSource")]
            Furniture furniture)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture forDelete = (from x in listAllFurniture where x.id == (int)Session["FurnitureCode"] select x).First();
            int index = listAllFurniture.IndexOf(forDelete);
            listAllFurniture.Remove(forDelete);
            //furniture.ImageSource = Session["PictureType"].ToString();
            listAllFurniture.Insert(index, furniture);
            WriteTXT.WriteFurniture(listAllFurniture);
            Session["FurnitureCode"] = null;
            //Session["PictureType"] = null;
            return RedirectToAction("Index");
        }

        //DELETE
        public ActionResult Delete(int id)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture deletePiece = (from f in listAllFurniture where f.id == id select f).FirstOrDefault();
            //Session["PieceToDelete"] = deletePiece.id;
            return View(deletePiece);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture deleteFurniture = (from f in listAllFurniture where f.id == (int)id select f).FirstOrDefault();
            listAllFurniture.Remove(deleteFurniture);
            WriteTXT.WriteFurniture(listAllFurniture);
            return RedirectToAction("Index", "Furniture");
        }

        //DETAILS
        public ActionResult Details(int id)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture piece = (from l in listAllFurniture where l.id == id select l).FirstOrDefault();
            return View(piece);
        }


        //EDIT
        public ActionResult Buy(int id)
        {
            List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
            //Furniture piece = (from p in listAllFurniture where p.id == id select p).First();
            int qua;
            for (int i = 0; i < listAllFurniture.Count; i++)
            {
                if (listAllFurniture[i].id == id )
                {
                    qua = Convert.ToInt32(listAllFurniture[i].quantity);
                    if (qua > 0)
                    {
                        qua -= 1;
                        listAllFurniture[i].quantity = qua.ToString();
                        break;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nemoze da kupite nema vise komada.");
                        break;
                    }
                }
            }
            //Session["FurnitureCode"] = piece.id;
            //ViewBag.Names = ReadTXT.ReadTXTFileAndFillListFC();
            return View();
        }
        //[HttpPost]
        //public ActionResult Buy([Bind(Include = "id, name, color, country, nameOfTheManufacturer, price, quantity, CategoryName, year, salon, ImageSource")]
        //    Furniture furniture)
        //{
        //    List<Furniture> listAllFurniture = ReadTXT.ReadTXTFileAndFillListFurniture();
        //    Furniture forDelete = (from x in listAllFurniture where x.id == (int)Session["FurnitureCode"] select x).First();
        //    int index = listAllFurniture.IndexOf(forDelete);
        //    listAllFurniture.Remove(forDelete);
        //    //furniture.ImageSource = Session["PictureType"].ToString();
        //    listAllFurniture.Insert(index, furniture);
        //    WriteTXT.WriteFurniture(listAllFurniture);
        //    Session["FurnitureCode"] = null;
        //    //Session["PictureType"] = null;
        //    return RedirectToAction("Index");
        //}

        //BUY
        public ActionResult Add2Cart(int id)
        {
            string Cartlocation = @"~/Cart.txt";  
            List<Cart> cartList = new List<Cart>();
            Furniture ft = new Furniture();
            List<Furniture> myList = ReadTXT.ReadTXTFileAndFillListFurniture();
            ft = myList.Where(x => x.id == id).Select(x => x).FirstOrDefault();
            int counter = 0;
            List<Furniture> myList3 = new List<Furniture>();
            myList3 = ReadTXT.ReadTXTFileAndFillListFurniture();
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

                    cartList.Add(cr);
                }
            }
            for (int i = 0; i < cartList.Count; i++)
            {
                if (cartList[i].Sifra == id)
                {
                    if (Convert.ToInt16(ft.quantity) > 0)
                    {
                        cartList[i].Kolicina++;
                        counter++;
                    }
                    else
                    {
                        TempData["NoMore"] = "Nema vise na stanju!";
                    }

                }
            }
            if (!cartList.Exists(x => x.Sifra == id))
            {
                Cart cr = new Cart();
                cr.Sifra = ft.id;
                cr.Naziv = ft.name;
                cr.Cena = Convert.ToDouble(ft.price);
                cr.Kolicina = 1;
                cr.Boja = ft.color;
                counter++;
                cartList.Add(cr);
            }
            int quantity ;
            for (int i = 0; i < myList3.Count; i++)
            {
                if (myList3[i].id == id)
                {
                    quantity = Convert.ToInt32(myList3[i].quantity);
                    quantity -= counter;
                    myList3[i].quantity = quantity.ToString();
                }
            }
            using (StreamWriter sw = new StreamWriter(Server.MapPath(Cartlocation)))
            {
                foreach (var item in cartList)
                {
                    sw.WriteLine(item.Sifra + "," + item.Naziv + "," + item.Boja + "," + item.Cena + "," + item.Kolicina);
                }
            }
            WriteTXT.WriteFurniture(myList3);
            TempData["Success"] = "Uspesno ste dodali u korpu.";

            return RedirectToAction("Index");
        }
    }
}