using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;

namespace StoreAPI.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Store;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne<Basket>()
            .WithOne()
            .HasForeignKey<Basket>(e => e.UserId)
            .IsRequired();
    
        modelBuilder.Entity<User>()
            .HasMany<Rate>()
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    
        modelBuilder.Entity<Product>()
            .HasMany<BookedProduct>()
            .WithOne()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
    
        modelBuilder.Entity<Product>()
            .HasMany<ProductInfo>()
            .WithOne()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
        
        modelBuilder.Entity<Product>()
            .HasMany<Rate>()
            .WithOne()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
        
        modelBuilder.Entity<Basket>()
            .HasMany<BookedProduct>()
            .WithOne()
            .HasForeignKey(e => e.BasketId)
            .IsRequired();
        
        modelBuilder.Entity<Brand>()
            .HasMany<Product>()
            .WithOne()
            .HasForeignKey(e => e.BrandId)
            .IsRequired();
        
        modelBuilder.Entity<Category>()
            .HasMany<Product>()
            .WithOne()
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BookedProduct> BookedProducts { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductInfo> ProductInfos { get; set; }
    public DbSet<Rate> Rates { get; set; }
}