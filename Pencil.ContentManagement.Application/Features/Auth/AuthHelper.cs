using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Pencil.ContentManagement.Application.Features.Auth;

public static class AuthHelper
{
    public static string GenerateJwtToken(Guid userId, IConfiguration configuration)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["Authentication:SecretForKey"]));

        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claimsForToken =
        [
            new Claim("UserId", userId.ToString()),
        ];

        var jwtSecurityToken = new JwtSecurityToken
        (
            configuration["Authentication:Issuer"],
            configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(7),
            signingCredentials
        );

        var tokenToReturn = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return tokenToReturn;
    }

    public static (bool Success, Guid UserId) GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var success = Guid.TryParse(httpContextAccessor.HttpContext.User?.FindFirst("UserId")?.Value, out var result);
        return (success, result);
    }

    public static bool IsUserAuthorized(IHttpContextAccessor httpContextAccessor, Guid userId)
        => userId.Equals(GetUserId(httpContextAccessor).UserId);

}