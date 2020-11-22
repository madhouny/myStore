using myStore.Models;
using myStore.myStoreServices;
using myStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace myStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var users = UserServices.Instance.GetUsers();
            return View(users);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                
                UserServices.Instance.SaveUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Some Error Occured");
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult Login(User user)
        {
            
                var obj = UserServices.Instance.GetUser(user);
                if(obj == null)
                {
                return View("Login", user);
                }
                else
                {
                    Session["UserID"] = obj.UserID;
                    Session["Username"] = obj.Username;
                    return RedirectToAction("LoggedIn");
                }
  
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

    }
}