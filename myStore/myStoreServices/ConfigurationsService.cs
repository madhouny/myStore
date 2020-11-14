using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.myStoreServices
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsService();

                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; }
        private ConfigurationsService() { }
        #endregion
        public Config GetConfig(string Key)
        {
            using(var context = new StoreContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}