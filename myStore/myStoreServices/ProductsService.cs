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
        public Product GetProducts(int Id)
        {
            using (var context = new StoreContext())
            {
                return context.Products.Find(Id);
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new StoreContext())
            {
                return context.Products.Include(c=>c.category).ToList();
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