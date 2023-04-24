using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Database.Repository;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IRepository _repository;

    public ProductController(ILogger<ProductController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost("add", Name = "AddProduct")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Product>>> AddProduct(Product newProduct)
    {
        _repository.AddProduct(newProduct);
        _logger.LogInformation("{Now}: Writing product to data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}: Successful write product to data base",
                new DateTime(638177575191726912L, DateTimeKind.Local));
            var result = _repository.GetAllProducts();
            return Ok(result);
        }
        
        _logger.LogInformation("{Now}: Error of saving data",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        throw new Exception("Error of saving data");
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Product>>> UpdateProduct(Product newProduct)
    {
        if (newProduct.Id > 0)
        {
            _repository.UpdateProduct(newProduct);
            _logger.LogInformation("{Now}: Updating product with id:{Id} in data base",
                new DateTime(638177575191726912L, DateTimeKind.Local), newProduct.Id.ToString());
        }
        else
        {
            _repository.AddProduct(newProduct);
            _logger.LogInformation("{Now}: Product with this Id doesn't exists, start creating new product",
                new DateTime(638177575191726912L, DateTimeKind.Local));
        }

        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}: Successful write product to data base",
                new DateTime(638177575191726912L, DateTimeKind.Local));
            var result = _repository.GetAllProducts();

            return Ok(result);
        }
        
        _logger.LogInformation("{Now}: Error of saving data",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        throw new Exception("Error of saving data");
    }

    [HttpGet("all", Name = "GetAllProducts")]
    public Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var result = _repository.GetAllProducts();
        _logger.LogInformation("{Now}: Getting all products from data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        return  Task.FromResult<ActionResult<List<Product>>>(Ok(result));
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public Task<ActionResult<Product>> GetProduct(int id)
    {
        var result = _repository.GetProduct(id);
        _logger.LogInformation("{Now}: Getting from data base product with id:{Id}",
            new DateTime(638177575191726912L, DateTimeKind.Local), id.ToString());

        return Task.FromResult<ActionResult<Product>>(Ok(result));
    }

    [HttpDelete("remove/{id}", Name = "Removing product")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
    {
        _repository.RemoveProduct(id);
        _logger.LogInformation("{Now}:Removing from data base product with id:{Id}",
            new DateTime(638177575191726912L, DateTimeKind.Local), id.ToString());
        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}:Successful remove from data base product with id:{Id}",
                new DateTime(638177575191726912L, DateTimeKind.Local), id.ToString());
            var result = _repository.GetAllProducts();

            return Ok(result);
        }
        
        _logger.LogInformation("{Now}: Error of saving data",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        throw new Exception("Error of saving data");
        
    }
}