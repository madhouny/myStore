using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.myStoreServices
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }
        private static ShopService instance { get; set; }
        private ShopService() { }
        #endregion

        public int SaveOrder(Order order)
        {
            using (var context = new StoreContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }

    }
}