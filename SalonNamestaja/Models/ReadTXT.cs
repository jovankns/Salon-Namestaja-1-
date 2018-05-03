using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class ReadTXT
    {
        //FURNITURE
        public static List<Furniture> GetAllBeds(string Search)
        {
            List<Furniture> list = new List<Furniture>();
            List<Furniture> context = ReadTXTFileAndFillListFurniture();
            list = (from x in context select x).ToList();
            if (!String.IsNullOrEmpty(Search))
            {
                list = list.Where(x =>
                {
                    var search = Search.ToLower();
                    var nameMatches = x.name != null && x.name.ToLower().Contains(search);
                    //var nameMatches = x.name != null && x.name.ToLower().StartsWith(search);
                    var colorMatches = x.color != null && x.color.ToLower().Contains(search);
                    var countryMatches = x.country != null && x.country.ToLower().Contains(search);
                    var nameManufactureMatches = x.nameOfTheManufacturer != null && x.nameOfTheManufacturer.ToLower().Contains(search);
                    //var priceMatches = x.price != null && x.price.Contains(search);
                    //var contentMatches = x.quantity != null && x.quantity.ToLower().Contains(search);
                    var typeMatches = x.CategoryName != null && x.CategoryName.ToLower().Contains(search);
                    //var yearmatches = x.year != null && x.year.ToLower().Contains(search);
                    return nameMatches || colorMatches || countryMatches || nameManufactureMatches ||
                        typeMatches ;
                }).ToList();
                return list;
            }
            else
            {
                return context;
            }
        }

        public static List<Furniture> ReadTXTFileAndFillListFurniture()//treba i search
        {
            int counter = 1;
            string path = HttpContext.Current.Server.MapPath("~/Namestaj.txt");
            if (File.Exists(path))
            {
                string line;
                List<Furniture> ftnList = new List<Furniture>();
                using (var reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            var splitList = line.Split(',');
                            if (Convert.ToInt16(splitList[6]) > 0)
                            {
                                Furniture ftn = new Furniture();
                                ftn.id =  counter++;
                                ftn.name = splitList[1];
                                ftn.color = splitList[2];
                                ftn.country = splitList[3];
                                ftn.nameOfTheManufacturer = splitList[4];
                                //ftn.price = Convert.ToDouble(splitList[5]);
                                //ftn.quantity = Convert.ToInt32(splitList[6]);
                                ftn.price = splitList[5];
                                ftn.quantity = splitList[6];
                                ftn.CategoryName = splitList[7];
                                //ftn.year = Convert.ToInt32(splitList[8]);
                                ftn.year = splitList[8];
                                ftn.salon = splitList[9];
                                ftn.ImageSource = splitList[10];

                                ftnList.Add(ftn);
                           }
                        }
                        else
                            continue;
                    }
                }
                return ftnList;
            }
            else
            {
                return null;
            }
        }

        //USERS
        public static List<User> GetAllUsers()
        {
            string path = HttpContext.Current.Server.MapPath("~/Korisnik.txt");
            List<User> list = new List<User>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() > 0)
                        {
                            string[] line = sr.ReadLine().Split(',');
                            if (line.Count() > 1)
                            {
                                User user = new User();
                                user.userName = line[0];
                                user.password = line[1];
                                user.name = line[2];
                                user.lastName = line[3];
                                user.role = line[4];
                                user.mobile = line[5];
                                user.email = line[6];
                                list.Add(user);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }


        //CATEGORIES
        public static List<FurnitureCategory> ReadTXTFileAndFillListFC()
        {
            string path = HttpContext.Current.Server.MapPath("~/Kategorija.txt");
            List<FurnitureCategory> list = new List<FurnitureCategory>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() > 0)
                        {
                            string[] line = sr.ReadLine().Split(',');
                            if (line.Length > 1)
                            {
                                FurnitureCategory category = new FurnitureCategory();
                                category.CategoryName = line[0];
                                category.CategoryDescription = line[1];
                                list.Add(category);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
    }
}