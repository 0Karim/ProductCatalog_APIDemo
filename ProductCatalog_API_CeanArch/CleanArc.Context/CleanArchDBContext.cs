using CleanArch.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CleanArc.Context
{
    public class CleanArchDBContext : DbContext
    {
        public CleanArchDBContext(DbContextOptions<CleanArchDBContext> options) : base(options)
        {}


        #region Tables

        public DbSet<Product> Products  { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData
                (
                 new Product() { Id = 1, Name = "Product 1", Price = 1000, LastUpdate = DateTime.Now },
                 new Product() { Id = 2, Name = "Product 2", Price = 2000, LastUpdate = DateTime.Now },
                 new Product() { Id = 3, Name = "Product 3", Price = 3000, LastUpdate = DateTime.Now },
                 new Product() { Id = 4, Name = "Product 4", Price = 4000, LastUpdate = DateTime.Now },
                 new Product() { Id = 5, Name = "Product 5", Price = 5000, LastUpdate = DateTime.Now },
                 new Product() { Id = 6, Name = "Product 6", Price = 6000, LastUpdate = DateTime.Now },
                 new Product() { Id = 7, Name = "Product 7", Price = 7000, LastUpdate = DateTime.Now },
                 new Product() { Id = 8, Name = "Product 8", Price = 8000, LastUpdate = DateTime.Now },
                 new Product() { Id = 9, Name = "Product 9", Price = 9000, LastUpdate = DateTime.Now },
                 new Product() { Id = 10, Name = "Product 10", Price = 10000, LastUpdate = DateTime.Now },
                 new Product() { Id = 11, Name = "Product 11", Price = 11000, LastUpdate = DateTime.Now },
                 new Product() { Id = 12, Name = "Product 12    ", Price = 12000, LastUpdate = DateTime.Now },
                 new Product() { Id = 13, Name = "Product 13", Price = 13000, LastUpdate = DateTime.Now },
                 new Product() { Id = 14, Name = "Product 14", Price = 14000, LastUpdate = DateTime.Now },
                 new Product() { Id = 15, Name = "Product 15", Price = 15000, LastUpdate = DateTime.Now },
                 new Product() { Id = 16, Name = "Product 16", Price = 16000, LastUpdate = DateTime.Now },
                 new Product() { Id = 17, Name = "Product 17", Price = 17000, LastUpdate = DateTime.Now },
                 new Product() { Id = 18, Name = "Product 18", Price = 18000, LastUpdate = DateTime.Now },
                 new Product() { Id = 19, Name = "Product 19", Price = 19000, LastUpdate = DateTime.Now },
                 new Product() { Id = 20, Name = "Product 20", Price = 20000, LastUpdate = DateTime.Now }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
