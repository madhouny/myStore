using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace myStore.myStoreServices
{
    public class CategoriesService
    {
        #region Singleton

        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();

                return instance;
            }
        }
        private static CategoriesService instance { get; set; }
        private CategoriesService() { }
        #endregion

        public Category GetCategories(int Id)
        {
            using (var context = new StoreContext())
            {
                return context.Categories.Find(Id);
            }
        }

        public int GetCategoriesCount(string search)
        {
            using(var context = new StoreContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                            category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var context = new StoreContext())
            {
                return context.Categories.ToList();
                //return context.Categories.Include(x => x.Products).ToList();
            }
        }

        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;

            using (var context = new StoreContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                  return context.Categories.Where(category => category.Name != null &&
                    category.Name.ToLower().Contains(search.ToLower()))
                      .OrderBy(x => x.ID)
                       .Skip((pageNo - 1) * pageSize)
                       .Take(pageSize)
                       .Include(x => x.Products)
                       .ToList();
                }
                else
                {
                 return context.Categories
                 .OrderBy(x => x.ID)
                 .Skip((pageNo - 1) * pageSize)
                 .Take(pageSize)
                 .Include(x => x.Products)
                 .ToList();
                 }
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
                //var category = context.Categories.Find(Id);
                var category = context.Categories.Where(x => x.ID == Id).Include(x => x.Products).FirstOrDefault();

                //Delete first product before deleting Category
                context.Products.RemoveRange(category.Products);

                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}