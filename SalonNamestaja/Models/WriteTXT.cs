using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class WriteTXT
    {
        public static void WriteFurniture(List<Furniture> list)
        {
            string path = HttpContext.Current.Server.MapPath("~/Namestaj.txt");
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in list)
                {
                    sw.WriteLine(item.id + "," + item.name + "," + item.color + "," + item.country + "," + item.nameOfTheManufacturer
                        + "," + item.price + "," + item.quantity + "," + item.CategoryName + "," + item.year + "," + item.salon + "," + item.ImageSource);
                }
            }
        }

        public static void WriteAllCategories(List<FurnitureCategory> list)
        {
            string path = HttpContext.Current.Server.MapPath("~/Kategorija.txt");
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in list)
                {
                    sw.WriteLine(item.CategoryName +","+ item.CategoryDescription);
                }
            }
        }
    }
}