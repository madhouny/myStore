using myStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.ViewModels
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }

    }

    public class CategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }
        
        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }


    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }
        [MinLength(5), MaxLength(200)]
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
}