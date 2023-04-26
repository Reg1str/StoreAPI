using Microsoft.AspNetCore.Mvc;
using StoreAPI.Database.Repository;
using StoreAPI.Models;
using StoreAPI.Services;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/sign")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IRepository _repository;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, IRepository repository, IAuthService authService, ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _authService = authService;
        _logger = logger;   
    }

    [HttpPost("in", Name = "Login")]
    public Task<ActionResult<string>> SignIn(LoginModel loginModel)
    {
        var tempUser = _repository.GetUser(loginModel.UserName);
        if (_authService.CheckPassword(tempUser, loginModel.Password))
        {
            var token = _authService.CreateToken(tempUser,
                _configuration.GetSection("AppSettings:TokenKey").Value!);
            return Task.FromResult<ActionResult<string>>(Ok(token));
        }
        
        return Task.FromResult<ActionResult<string>>(BadRequest("Username or password are wrong or user doesn't exists"));
    }
    
    [HttpPost("up", Name = "Register")]
    public async Task<ActionResult<User>> SignUp(RegisterModel registerModel)
    {
        _logger.LogInformation("{Now}: Register of new user",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        var user = new User();
        
        _authService.CreatePassword(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        _logger.LogInformation("{Now}: Password was hashed",
            new DateTime(638177575191726912L, DateTimeKind.Local));

        user.UserName = registerModel.UserName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        _repository.AddUser(user);
        
        _logger.LogInformation("{Now}: Adding user for data base",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        if (await _repository.SaveChangesAsync())
        {
            _logger.LogInformation("{Now}: Successful add user to data base",
                new DateTime(638177575191726912L, DateTimeKind.Local));
            
            return Ok(user);
        }

        _logger.LogInformation("{Now}: Error of user saving",
            new DateTime(638177575191726912L, DateTimeKind.Local));
        
        return BadRequest("Error of data saving");

    }
    
    // [HttpGet("check", Name = "Token check")]
    // public async Task<IActionResult> Check()
    // {
    //     
    // }
}