using SalonNamestaja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonNamestaja.Controllers
{
    public class FurnitureCategoryController : Controller
    {
        static List<FurnitureCategory> listAllCategories = new List<FurnitureCategory>();

        // GET: Category
        public ActionResult Index()
        {
            listAllCategories = ReadTXT.ReadTXTFileAndFillListFC();
            return View(listAllCategories);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "CategoryName, CategoryDescription")]FurnitureCategory category)
        {
            bool doesContain = listAllCategories.Contains(category);
            if (doesContain == false)
            {
                listAllCategories.Add(category);
                AllMethods.AddNewCategory(category);
            }
            return RedirectToAction("Index", "FurnitureCategory");
        }

        public ActionResult Edit(string name)
        {
            FurnitureCategory category = (from c in listAllCategories where c.CategoryName == name select c).First();
            Session["EditThisCategory"] = category;
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CategoryName,CategoryDescription")]FurnitureCategory category)
        {
            FurnitureCategory deleteCategory = Session["EditThisCategory"] as FurnitureCategory;
            int index = listAllCategories.IndexOf(deleteCategory);
            listAllCategories.Remove(deleteCategory);
            listAllCategories.Insert(index, category);
            //listAllCategories.Add(category);
            WriteTXT.WriteAllCategories(listAllCategories);
            Session["EditThisCategory"] = null;

            return RedirectToAction("Index", "FurnitureCategory");
        }

        public ActionResult Delete(string name)
        {
            FurnitureCategory deleteCategory = (from c in listAllCategories where c.CategoryName == name select c).First();
            return View(deleteCategory);
        }
        [HttpPost]
        public ActionResult Delete(string name, string decription)
        {
            FurnitureCategory deleteCategory = (from c in listAllCategories
                                                where c.CategoryName == name
                                                select c).First();

            listAllCategories.Remove(deleteCategory);
            WriteTXT.WriteAllCategories(listAllCategories);
            return RedirectToAction("Index", "FurnitureCategory");
        }
    }
}