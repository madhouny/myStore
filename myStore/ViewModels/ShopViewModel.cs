using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.ViewModels
{
    public class ChekoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }

        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public int MaxPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }

        public int? SortBy { get; set; }

        public int? CategoryID { get; set; }

        public Pager Pager { get; set; }

        public string SearchTerm { get; set; }

    }

    public class FilterProductsShopViewModel
    {
       
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }

        public int? SortBy { get; set; }

        public int? CategoryID { get; set; }

        public string SearchTerm { get; set; }


    }
}