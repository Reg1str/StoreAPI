using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Database.Repository;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IRepository _repository;

    public BrandController(ILogger<BrandController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    [HttpPost("add", Name = "AddBrand")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Brand>>> AddBrand (Brand newBrand)
    {
        _repository.AddBrand(newBrand);
        _logger.LogInformation("{Now}: Writing brand to data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}: Successful write brand to data base",
                new DateTime(638177575071869031L, DateTimeKind.Local));
            var result = _repository.GetAllBrands();
            return Ok(result);
        }
        
        _logger.LogInformation("{Now}: Error of saving data",
            new DateTime(638177575191726912L, DateTimeKind.Local));

        throw new Exception("Error of saving data");
    }
    
    [HttpGet("all", Name = "GetAllBrands")]
    public Task<ActionResult<List<Brand>>> GetAllBrands()
    {
        var result = _repository.GetAllBrands();
        _logger.LogInformation("{Now}: Getting all brands from data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        return  Task.FromResult<ActionResult<List<Brand>>>(Ok(result));
    }
    
    [HttpGet("{id}", Name = "GetBrand")]
    public Task<ActionResult<Brand>> GetBrand(int id)
    {
        var result = _repository.GetBrand(id);
        _logger.LogInformation("{Now}: Getting from data base brand with id:{Id}",
            new DateTime(638177575191726912L, DateTimeKind.Local), id.ToString());
        
        return Task.FromResult<ActionResult<Brand>>(Ok(result));
    }
}