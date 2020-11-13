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

        //Injection de Service Categories, Products , pour separer Controller et Database
        //ProductsService productService = new ProductsService();
        CategoriesService categoryService = new CategoriesService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            //var products = productService.GetProducts();

            ProductSearchViewModel model = new ProductSearchViewModel();

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = ProductsService.Instance.GetProducts(model.PageNo);
            
            if(string.IsNullOrEmpty(search) == false)
            {
                //products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
           
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Recuperer les categories
            //CategoriesService categoryService = new CategoriesService();
            //var categories = categoryService.GetCategories();
            //return PartialView(categories);

            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryID = model.CategoryID;
            newProduct.category = categoryService.GetCategories(model.CategoryID);

            ProductsService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            //var product = productService.GetProducts(Id);
            //return PartialView(product);

            EditProductViewModel model = new EditProductViewModel();
            var product = ProductsService.Instance.GetProduct(Id);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.category != null ? product.category.ID : 0;
            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);
 
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            //productService.UpdateProduct(product);
            //return RedirectToAction("ProductTable");

            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.category = categoryService.GetCategories(model.CategoryID);

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

     
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductsService.Instance.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }


    }
}