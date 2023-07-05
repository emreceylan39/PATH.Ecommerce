using LoginService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Domain.Contexts
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions options) : base(options)
        {
        }

        public LoginDbContext()
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //base.OnConfiguring(optionsBuilder);
             => optionsBuilder.UseSqlServer("Server=.\\SQL22; Database=LoginService; User Id=sa; Password=master.12; TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User { Id = 1, Email = "user1@mail.com", Password="user1", FirstName = "User", LastName = "Bir", CreatedDate=DateTime.Now },
                    new User { Id = 2, Email = "user2@mail.com", Password="user2", FirstName = "User", LastName = "Iki", CreatedDate = DateTime.Now },
                    new User { Id = 3, Email = "user3@mail.com", Password="user3", FirstName = "User", LastName = "Uc", CreatedDate = DateTime.Now }
                );

        }

    }
}
