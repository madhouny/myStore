using myStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.ViewModels
{
    public class NewUserViewModel
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "FirstName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

    }

    public class UsersViewModel
    {
        public List<User> Users { get; set; }
        
    }
}