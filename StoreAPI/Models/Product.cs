namespace StoreAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public double Rating { get; set; }
    public string? Img { get; set; }
    public string? CategoryId { get; set; }
    public string? BrandId { get; set; }
}