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
            //Recuperer les categories
            CategoriesService categoryService = new CategoriesService();

            var categories = categoryService.GetCategories();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            //Injection de service
            CategoriesService categoryService = new CategoriesService();

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryID = model.CategoryID;
            newProduct.category = categoryService.GetCategories(model.CategoryID);

            productService.SaveProduct(newProduct);
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