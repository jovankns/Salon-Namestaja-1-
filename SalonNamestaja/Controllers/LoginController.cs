using SalonNamestaja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonNamestaja.Controllers
{
    public class LoginController : Controller
    {
        List<User> allUsers = ReadTXT.GetAllUsers();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Password")]User user)
        {
            User users = (from l in allUsers
                             where l.userName == user.userName
                             where l.password == user.password
                             select l).FirstOrDefault();
            if (users != null)
            {
                if (users.role.TrimStart() == "admin")
                {
                    Session["Admin"] = true;
                    Session["LogedUser"] = users;
                    return RedirectToAction("Index", "Furniture");
                }
                else
                {
                    Session["Admin"] = false;
                    Session["LogedUser"] = users;
                    return RedirectToAction("Index", "Furniture");
                }
            }
            else
            {
                //TempData["Success"] = "Neispravan unos";
                ModelState.AddModelError("", "Username or password is wrong!");
                //return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            //ShopingController.allBills.Clear();
            //ShopingController.shopingList.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}