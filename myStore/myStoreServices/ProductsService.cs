using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace myStore.myStoreServices
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();

                return instance;
            }
        }
        private static ProductsService instance { get; set; }
        private ProductsService() { }
        #endregion

        public Product GetProduct(int Id)
        {
            using (var context = new StoreContext())
            {
                //return context.Products.Find(Id);
                return context.Products.Where(x => x.ID == Id).Include(x => x.category).FirstOrDefault();
            }
        }

        // Checkout
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new StoreContext())
            {
                
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 3;
                
            using (var context = new StoreContext())
            {
               
                //return context.Products.OrderBy(x=>x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(c=>c.category).ToList();
                return context.Products.Include(x => x.category).ToList();

            }
        }

        public void SaveProduct(Product product)
        {
            using(var context = new StoreContext())
            {
                //Save Category in Database
                context.Entry(product.category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new StoreContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int Id)
        {
            using (var context = new StoreContext())
            {
                var product = context.Products.Find(Id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}