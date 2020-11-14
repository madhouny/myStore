using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.Models
{
    public class Product
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public int CategoryID { get; set; }
        public virtual Category category { get; set; }
        public string ImageURL { get; set; }

    }
}