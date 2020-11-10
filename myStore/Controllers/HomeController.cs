using myStore.myStoreServices;
using myStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myStore.Controllers
{
    public class HomeController : Controller
    {
        //Injection de Service Categories , pour separer Controller et Database
        CategoriesService categoryService = new CategoriesService();

        public ActionResult Index()
        {
            //Creer une nouvelle instance de ViewModel
            HomeViewModel model = new HomeViewModel();

            // Ajouter la liste des categories dans le ViewModel
            model.FeaturedCategories = categoryService.GetFeaturedCategories();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}