using myStore.ViewModels;
using myStore.myStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myStore.Controllers
{
    public class WidgetsController : Controller
    {
        
        public ActionResult Products(bool isLatestProducts)
        {
            //Injecter le Service ProductsWidgetViewModel
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                model.Products = ProductsService.Instance.GetLatestProducts(4);
            }
            else
            {
                model.Products = ProductsService.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }
    }
}