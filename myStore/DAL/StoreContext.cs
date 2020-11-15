using myStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myStore.DAL
{
    public class StoreContext : DbContext 
    {
        //API VALIDATIONS FOR CATEGORIES
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Config> Configurations { get; set; }

    }
}