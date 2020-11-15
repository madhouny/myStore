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
        //CategoriesService categoryService = new CategoriesService();


        [HttpGet]
        public ActionResult Index()
        {
            //var categories = categoryService.GetCategories();
            //return View(categories);
            return View();
        }
        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            //model.Categories = CategoriesService.Instance.GetCategories();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
            model.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value);

            //if (!string.IsNullOrEmpty(search))
            //{
            //    model.SearchTerm = search;
            //    model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            //    //categories = categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            //}

            if(model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);
                return PartialView("CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
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
            if (ModelState.IsValid)
            {


                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.isFeatured = model.isFeatured;

                CategoriesService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesService.Instance.GetCategories(Id);

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
            if (ModelState.IsValid)
            {
                var existingCategory = CategoriesService.Instance.GetCategories(model.ID);
                existingCategory.Name = model.Name;
                existingCategory.Description = model.Description;
                existingCategory.ImageURL = model.ImageURL;
                existingCategory.isFeatured = model.isFeatured;

                CategoriesService.Instance.UpdateCategory(existingCategory);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion

        [HttpPost]
        public ActionResult Delete(int  Id)
        {
            CategoriesService.Instance.DeleteCategory(Id);
            return RedirectToAction("CategoryTable");
        }
    }
}