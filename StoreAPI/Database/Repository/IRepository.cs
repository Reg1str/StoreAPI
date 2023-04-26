using StoreAPI.Models;

namespace StoreAPI.Database.Repository;

public interface IRepository
{
    User GetUser(int id);
    User GetUser(string name);
    Product GetProduct(int id);
    List<Product> GetAllProducts();
    Basket GetBasket(int id);
    Brand GetBrand(int id);
    List<Brand> GetAllBrands();
    Category GetCategory(int id);
    List<Category> GetAllCategories();
    ProductInfo GetProductInfo(int id);
    Rate GetRate(int id);
    BookedProduct GetBookedProduct(int id);
    void AddUser(User user);
    void AddProduct(Product product);
    void RemoveProduct(int id);
    void UpdateProduct(Product product);
    void AddCategory(Category category);
    void AddBrand(Brand brand);
    void RemoveCategory(int id);
    Task<bool> SaveChangesAsync();
}