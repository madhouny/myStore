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

                return context.Products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                //return context.Products.Include(x => x.category).ToList();

            }
        }

        // Products forWidget 
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
          
            using (var context = new StoreContext())
            {

                return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                //return context.Products.Include(x => x.category).ToList();

            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {

            using (var context = new StoreContext())
            {

                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.category).ToList();
                

            }
        }

        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new StoreContext())
            {

                return context.Products.Where(x=>x.category.ID == categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.category).ToList();
                
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

        public int GetMaxPrice()
        {
            using (var context = new StoreContext())
            {
                return (int)(context.Products.Max(x => x.Price));

            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy, int pageNo, int pageSize)
        {
            using (var context = new StoreContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minPrice).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxPrice).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                       
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            }
        }

        public int SearchProductsCount(string searchTerm, int? minPrice, int? maxPrice, int? categoryID, int? sortBy)
        {
            using (var context = new StoreContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minPrice).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxPrice).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {

                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;

                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                return products.Count;

            }
        }

    }
}