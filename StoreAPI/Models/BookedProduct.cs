namespace StoreAPI.Models;

public class BookedProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int BasketId { get; set; }
}