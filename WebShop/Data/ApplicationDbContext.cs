using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Models;

namespace WebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {



        }
    
        public DbSet<Product> Products  { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<UserRating> UserRating { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });

        }









    }

        public class ApplicationUser :IdentityUser
        {
            [StringLength(50)]
            public string? FirstName { get;set; }
            [StringLength(50)]
            public string? LastName { get;set; }
            [StringLength(150)]
            public string? Address { get;set; }


            [ForeignKey("UserId")]
            public List<Order> Orders { get; set; }

        }   






}