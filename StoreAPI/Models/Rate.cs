namespace StoreAPI.Models;

public class Rate
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public double Mark { get; set; }
}