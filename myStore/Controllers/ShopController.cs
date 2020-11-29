using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myStore.CODE;
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
   
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaxPrice = ProductsService.Instance.GetMaxPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);
          
            return View(model);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            ChekoutViewModel model = new ChekoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }
        public ActionResult FilterProducts (string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            FilterProductsShopViewModel model = new FilterProductsShopViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, categoryID, sortBy, pageNo.Value, pageSize);
  
            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }

        // return a JSON Result
        public JsonResult PlaceOrder(string productIDs)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if(!string.IsNullOrEmpty(productIDs))
            {

                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();
                var boughtProducts = ProductsService.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantities.Where(productID => productID == x.ID).Count() }));

                var rowEffected = ShopService.Instance.SaveOrder(newOrder);

                result.Data = new { Success = true, Rows = rowEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}