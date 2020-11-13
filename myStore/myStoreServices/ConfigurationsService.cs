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
        public Config GetConfig(string Key)
        {
            using(var context = new StoreContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}