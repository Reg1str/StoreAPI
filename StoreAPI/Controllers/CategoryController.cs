using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Database.Repository;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IRepository _repository;
    public CategoryController(ILogger<CategoryController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost("add", Name = "AddCategory")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Category>>> AddCategory (Category newCategory)
    {
        _repository.AddCategory(newCategory);
        _logger.LogInformation("{Now}: Writing category to data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}: Successful write category to data base",
                new DateTime(638177575071869031L, DateTimeKind.Local));
            var result = _repository.GetAllCategories();
            return Ok(result);
        }
        
        _logger.LogInformation("{Now}: Error of saving data",
            new DateTime(638177575191726912L, DateTimeKind.Local));

        throw new Exception("Error of saving data");
    }
    
    [HttpGet("all", Name = "GetAllCategories")]
    public Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var result = _repository.GetAllCategories();
        _logger.LogInformation("{Now}: Getting all categories from data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        return  Task.FromResult<ActionResult<List<Category>>>(Ok(result));
    }
    
    [HttpGet("{id}", Name = "GetCategory")]
    public Task<ActionResult<Category>> GetCategory(int id)
    {
        var result = _repository.GetCategory(id);
        _logger.LogInformation("{Now}: Getting from data base category with id:{Id}",
            new DateTime(638177575191726912L, DateTimeKind.Local), id.ToString());
        
        return Task.FromResult<ActionResult<Category>>(Ok(result));
    }
}