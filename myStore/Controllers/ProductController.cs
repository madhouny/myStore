using myStore.Models;
using myStore.myStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myStore.Controllers
{
    public class ProductController : Controller
    {

        //Injection de Service Categories , pour separer Controller et Database
        ProductsService productService = new ProductsService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var products = productService.GetProducts();
            if(string.IsNullOrEmpty(search) == false)
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
           
            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            productService.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = productService.GetProducts(Id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

     
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            productService.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }


    }
}