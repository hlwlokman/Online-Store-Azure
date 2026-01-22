using Microsoft.EntityFrameworkCore;
using WidgetStore.Models;

namespace WidgetStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Iphone 17",
                    Description = "Phone",
                    Price = 99.99m,
                    StockQuantity = 100,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Widget+Pro",
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Asus Zenbook",
                    Description = "Laptop",
                    Price = 49.99m,
                    StockQuantity = 200,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Widget+Lite",
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Samsung TV",
                    Description = "TV",
                    Price = 149.99m,
                    StockQuantity = 50,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Widget+Ultra",
                    CreatedDate = DateTime.UtcNow
                }
            );
        }
    }
}