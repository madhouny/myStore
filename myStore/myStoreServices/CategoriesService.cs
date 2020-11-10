using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.myStoreServices
{
    public class CategoriesService
    {
        public Category GetCategories(int Id)
        {
            using (var context = new StoreContext())
            {
                return context.Categories.Find(Id);
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new StoreContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new StoreContext())
            {
                return context.Categories.Where(x=>x.isFeatured && x.ImageURL != null).ToList();
            }
        }

        public void SaveCategory(Category category)
        {
            using(var context = new StoreContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new StoreContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int Id)
        {
            using (var context = new StoreContext())
            {
                var category = context.Categories.Find(Id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}