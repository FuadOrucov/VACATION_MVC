using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using VOCATION_MVC.Models;

namespace VOCATION_MVC.Controllers
{
   
    public class LoginController : Controller
    {
        Vacation_DBEntities db = new Vacation_DBEntities();
        [AllowAnonymous]
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public ActionResult Index(Tbl_Account account)
        {
            bool result = IsValid(account.Mail, account.Password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(account.Mail, false);
                 return RedirectToAction("GetProfile", "Persons", new RouteValueDictionary(new { Controller = "Persons", Action = "GetProfile", mail=account.Mail }));

            }
            else
            {
                ViewBag.message = "Login Faild";
                return View();
            }
        }
        public bool IsValid(string mail, string password)
        {
            bool Isvalid = false;
            var user = db.Tbl_Account.FirstOrDefault(o => o.Mail == mail);
            if (user != null)
            {
                if (user.Password == password)
                {
                    Isvalid = true;
                }
            }
            return Isvalid;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}

