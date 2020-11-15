using myStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
    }
    public class ProductSearchViewModel
    {
        public List<Product> Products { get;  set; }
        public string SearchTerm { get; set; }
        public int PageNo { get;  set; }

    }

    public class NewProductViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }


        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int ID { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }

}