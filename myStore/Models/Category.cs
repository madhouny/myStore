using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.Models
{
    public class Category
    {
        public int ID { get; set; } 

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        
        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
        public string ImageURL { get; set; }
        public bool isFeatured { get; set; }
    }
}