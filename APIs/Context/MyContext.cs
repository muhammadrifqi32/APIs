using APIs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace APIs.Context
{
    // Define a class named MyContext that inherits from DbContext.
    // Inheriting from DbContext provides the foundation for creating a custom
    // database context for your application.
    public class MyContext : DbContext
    {
        // Define a constructor that takes DbContextOptions as a parameter and calls the base constructor with the options.
        // This constructor is used to configure the database context and specify the database connection details.
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        // Define a DbSet property named Departments to represent a collection of Department entities.
        // This property allows you to interact with the "Department" entities and perform database operations (e.g., querying, inserting, updating, and deleting).
        public DbSet<Department> Departments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Division>()
        //        .HasMany(dept => dept.Departments)
        //        .WithOne(div => div.division);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
