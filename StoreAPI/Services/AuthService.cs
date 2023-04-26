using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Models;

namespace StoreAPI.Services;

public class AuthService : IAuthService
{
    public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool CheckPassword(User user, string password)
    {
        byte[] computedHash;
        
        using var hmac = new HMACSHA512(user.PasswordSalt);
        {
            computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        return computedHash.SequenceEqual(user.PasswordHash);
    }

    public string CreateToken(User user, string tokenKey)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}