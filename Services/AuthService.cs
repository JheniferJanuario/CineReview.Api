using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CineReview.Api.Services;
public interface IAuthService
{
    string GerarToken(string email);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string GerarToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["AppSettings:JwtKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
