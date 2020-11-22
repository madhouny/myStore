using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [Display(Name ="Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "FirstName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name ="Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }
    }
}