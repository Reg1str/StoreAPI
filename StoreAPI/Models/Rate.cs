namespace StoreAPI.Models;

public class Rate
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ProductId { get; set; }
    public double Mark { get; set; }
}