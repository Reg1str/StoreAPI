using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/sign")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    // [HttpPost("in", Name = "Login")]
    // public async Task<IActionResult> SignIn()
    // {
    //     
    // }
    //
    // [HttpPost("up", Name = "Register")]
    // public async Task<IActionResult> SignUp()
    // {
    //     
    // }
    //
    // [HttpGet("check", Name = "Token check")]
    // public async Task<IActionResult> Check()
    // {
    //     
    // }
}