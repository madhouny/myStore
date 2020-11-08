﻿using myStore.Models;
using myStore.myStoreServices;
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
            var categories = categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.SaveCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = categoryService.GetCategories(Id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var category = categoryService.GetCategories(Id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryService.DeleteCategory(category.ID);
            return RedirectToAction("Index");
        }
    }
}