using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _4Create.Domain.Aggregates.Users.Enums;
using Microsoft.IdentityModel.Tokens;

namespace _4Create.Infrastructure.Services.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;
    private readonly string _audience;
    private readonly string _issuer;
    private readonly string _signingKey;
    private readonly int _expirationTimeInMinutes;


    public JwtTokenService(
        JwtSecurityTokenHandler jwtTokenHandler,
        string audience,
        string issuer,
        string signingKey,
        int expirationTimeInMinutes)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _audience = audience;
        _issuer = issuer;
        _signingKey = signingKey;
        _expirationTimeInMinutes = expirationTimeInMinutes;
    }

    public string GenerateJwtToken(Guid id, string username, UserRole role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
            signingCredentials: credentials);

        return _jwtTokenHandler.WriteToken(token);
    }
}
