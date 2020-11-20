using myStore.CODE;
using myStore.myStoreServices;
using myStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myStore.Controllers
{
    public class ShopController : Controller
    {
        
        public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int?sortBy)
        {
            ShopViewModel model = new ShopViewModel();
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaxPrice = ProductsService.Instance.GetMaxPrice();

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy);

            model.SortBy = sortBy;
          
            return View(model);
        }
        public ActionResult Checkout()
        {
            ChekoutViewModel model = new ChekoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIDs);

            }
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
        {
            FilterProductsShopViewModel model = new FilterProductsShopViewModel();
            
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy);

            return PartialView(model);
        }
    }
}