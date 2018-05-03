using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class AllMethods
    {
        public static List<Furniture> AddFurniture(Furniture addF)
        {
            //string path = HttpContext.Current.Server.MapPath("~/Namestaj.txt");
            List<Furniture> list = ReadTXT.ReadTXTFileAndFillListFurniture();
            Furniture newFtn = new Furniture();
            newFtn.name = addF.name;
            newFtn.color = addF.color;
            newFtn.country = addF.country;
            newFtn.nameOfTheManufacturer = addF.nameOfTheManufacturer;
            newFtn.price = addF.price;
            newFtn.quantity = addF.quantity;
            newFtn.CategoryName = addF.CategoryName;
            newFtn.year = addF.year;
            newFtn.salon = addF.salon;
            newFtn.ImageSource = addF.ImageSource;
            list.Add(newFtn);

            WriteTXT.WriteFurniture(list);
            //using (StreamWriter sw = new StreamWriter(path))
            //{
            //    foreach (var item in list)
            //    {
            //        sw.WriteLine(item.id + "," + item.name + "," + item.color + "," + item.country + "," + item.nameOfTheManufacturer
            //            + "," + item.price + "," + item.quantity + "," + item.CategoryName + "," + item.year + "," + item.salon + "," + item.ImageSource);
            //    }
            //}
            return list;
            //TempData["Success"] = "Uspesno ste obrisali iz korpe.";
        }

        public static void AddNewCategory(FurnitureCategory category)
        {
            string path = HttpContext.Current.Server.MapPath("~/Kategorija.txt");
            if (File.Exists(path))
            {
                try
                {
                    string line = category.CategoryName + ", " + category.CategoryDescription;
                    File.AppendAllText(path, Environment.NewLine + line);
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
    }
}