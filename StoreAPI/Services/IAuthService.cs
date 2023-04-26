using StoreAPI.Models;

namespace StoreAPI.Services;

public interface IAuthService
{
    public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool CheckPassword(User user, string password);
    public string CreateToken(User user, string tokenKey);
}