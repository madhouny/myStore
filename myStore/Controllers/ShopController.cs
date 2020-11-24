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
        
        public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaxPrice = ProductsService.Instance.GetMaxPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy, pageNo.Value, 6);

            model.SortBy = sortBy;

            model.CategoryID = categoryID;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryID, sortBy);

            model.Pager = new Pager(totalCount, pageNo);
          
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
        public ActionResult FilterProducts (string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            FilterProductsShopViewModel model = new FilterProductsShopViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryID, sortBy);

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy, pageNo.Value, 6);
            

            model.Pager = new Pager(totalCount, pageNo);

            return PartialView(model);
        }
    }
}