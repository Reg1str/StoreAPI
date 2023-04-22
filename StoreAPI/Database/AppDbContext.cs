using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;

namespace StoreAPI.Database;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost;Database=Store;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BookedProduct> BookedProducts { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductInfo> ProductInfos { get; set; }
    public DbSet<Rate> Rates { get; set; }
}