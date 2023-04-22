using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;

    public BrandController(ILogger<BrandController> logger)
    {
        _logger = logger;
    }
}