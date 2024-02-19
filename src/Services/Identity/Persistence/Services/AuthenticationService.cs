using Application.Contracts;
using Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Services;

public class AuthenticationService(IOptions<JwtSettings> jwtSettings) : IAuthenticationService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public AuthenticationResponse Authenticate(AuthenticationRequest request)
    {
        var userName = (request?.UserName) ?? throw new Exception($"User is invalid.");
        if (request?.Password != "P@$$w0rd")
        {
            throw new Exception($"Credentials for '{userName} aren't valid'.");
        }

        JwtSecurityToken jwtSecurityToken = GenerateToken(userName);

        AuthenticationResponse response = new()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        };

        return response;
    }

    private JwtSecurityToken GenerateToken(string userName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Name, userName),
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}
