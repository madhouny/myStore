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
  
    public class CategoryController : Controller
    {
        //Injection de Service Categories , pour separer Controller et Database
        CategoriesService categoryService = new CategoriesService();


        [HttpGet]
        public ActionResult Index()
        {
            //var categories = categoryService.GetCategories();
            //return View(categories);
            return View();
        }
        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.Categories = categoryService.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
                //categories = categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView("CategoryTable", model);
        }

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {

            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.isFeatured = model.isFeatured;

            categoryService.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = categoryService.GetCategories(Id);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = categoryService.GetCategories(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            categoryService.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }
        #endregion

        [HttpPost]
        public ActionResult Delete(int  Id)
        {
            categoryService.DeleteCategory(Id);
            return RedirectToAction("CategoryTable");
        }
    }
}