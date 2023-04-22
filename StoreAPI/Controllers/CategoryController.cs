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

    [HttpPost("add", Name = "Add")]
    public async Task<ActionResult<List<Category>>> AddCategory (Category newCategory)
    {
        _repository.AddCategory(newCategory);
        if (await _repository.SaveChangesAsync())
        {
            var result = _repository.GetAllCategories();
            return Ok(result);
        }

        throw new Exception("Error of saving data");
    }
    
    [HttpGet("all", Name = "All categories")]
    public Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var result = _repository.GetAllCategories();
        
        return  Task.FromResult<ActionResult<List<Category>>>(Ok(result));
    }
    
    [HttpGet("{id}", Name = "Category")]
    public Task<ActionResult<Category>> GetCategory(int id)
    {
        var result = _repository.GetCategory(id);
        
        return Task.FromResult<ActionResult<Category>>(Ok(result));
    }
}