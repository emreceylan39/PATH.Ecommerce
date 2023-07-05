using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.DbContexts
{
    public class CatalogServiceDbContext : DbContext
    {
        public CatalogServiceDbContext()
        {
        }

        public CatalogServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             //base.OnConfiguring(optionsBuilder);
             => optionsBuilder.UseSqlServer("Server=.\\SQL22; Database=CatalogService; User Id=sa; Password=master.12; TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products);

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category() { Id = 1, Name = "Foods" },
                    new Category() { Id = 2, Name = "Drinks" },
                    new Category() { Id = 3, Name = "Books" }
                    );
            modelBuilder.Entity<Product>()
                .HasData(
                new Product() { Id = 1, Name = "Microservice Architecture", CategoryId = 3, Price=100, UnitsInStock=10 },
                new Product() { Id = 2, Name = "Tuborg Gold", CategoryId = 2, Price=37, UnitsInStock=999 },
                new Product() { Id = 3, Name = "Fıstık", CategoryId = 1, Price=22, UnitsInStock=25 },
                new Product() { Id = 4, Name = "C#", CategoryId = 3, Price=35, UnitsInStock=33 },
                new Product() { Id = 5, Name = "Yeni Raki 100cl.", CategoryId = 2, Price=44, UnitsInStock=54 },
                new Product() { Id = 6, Name = "Beyaz Peynir", CategoryId = 1 }
                );
        }
    }
}
