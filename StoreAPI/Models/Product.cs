namespace StoreAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public double Rating { get; set; }
    public string? Img { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
}