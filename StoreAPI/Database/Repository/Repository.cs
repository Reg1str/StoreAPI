using StoreAPI.Models;

namespace StoreAPI.Database.Repository;

public class Repository : IRepository
{
    private readonly AppDbContext _ctx;

    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    public Product GetProduct(int id)
    {
        return _ctx.Products
            .FirstOrDefault(p => p.Id == id) ??
               throw new Exception("Products list is empty");
    }

    public List<Product> GetAllProducts()
    {
        return _ctx.Products
            .ToList();
    }

    public Basket GetBasket(int id)
    {
        return _ctx.Baskets
            .FirstOrDefault(b => b.Id == id) ??
               throw new Exception("There are no baskets");
    }

    public Brand GetBrand(int id)
    {
        return _ctx.Brands
            .FirstOrDefault(b => b.Id == id) ??
               throw new Exception("There are no brands");
    }

    public List<Brand> GetAllBrands()
    {
        return _ctx.Brands
            .ToList();
    }

    public Category GetCategory(int id)
    {
        return _ctx.Categories
            .FirstOrDefault(c => c.Id == id) ??
               throw new Exception("There are no categories");
    }

    public List<Category> GetAllCategories()
    {
        return _ctx.Categories
            .ToList();
    }

    public ProductInfo GetProductInfo(int id)
    {
        return _ctx.ProductInfos
            .FirstOrDefault(p=>p.Id == id) ??
               throw new Exception("There are no info about products");
    }

    public Rate GetRate(int id)
    {
        return _ctx.Rates
            .FirstOrDefault(r=>r.Id == id) ?? throw new Exception("There are no rates");
    }

    public BookedProduct GetBookedProduct(int id)
    {
        return _ctx.BookedProducts
            .FirstOrDefault(b=>b.Id == id) ??
               throw new Exception("There are no booked products");
    }

    public void AddProduct(Product product)
    {
        _ctx.Products.Add(product);
    }

    public void RemoveProduct(int id)
    {
        _ctx.Products.Remove(GetProduct(id));
    }

    public void UpdateProduct(Product product)
    {
        _ctx.Products.Update(product);
    }

    public void AddCategory(Category category)
    {
        _ctx.Categories.Add(category);
    }

    public void AddBrand(Brand brand)
    {
        _ctx.Brands.Add(brand);
    }

    public void RemoveCategory(int id)
    {
        _ctx.Categories.Remove(GetCategory(id));
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _ctx.SaveChangesAsync() > 0;
    }
}