﻿using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.myStoreServices
{
    public class ConfigurationsService
    {
        //public static ConfigurationsService ClassObject { 
        //    get {
        //        if (privateInMemoryObject == null) privateInMemoryObject = new ConfigurationsService();

        //        return privateInMemoryObject;
        //    }
        //}
        //private static ConfigurationsService privateInMemoryObject { get; set; }
        //private ConfigurationsService() {}
        public Config GetConfig(string Key)
        {
            using(var context = new StoreContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}