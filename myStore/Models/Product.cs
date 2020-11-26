using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(1,100000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category category { get; set; }
        public string ImageURL { get; set; }

    }
}