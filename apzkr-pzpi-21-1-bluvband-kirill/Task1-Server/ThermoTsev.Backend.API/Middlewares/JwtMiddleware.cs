using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.API.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context, IJwtService jwtService)
    {
        string? token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ")[^1];

        if (token != null)
            AttachUserToContext(context, jwtService, token);

        return next(context);
    }

    private static void AttachUserToContext(HttpContext context, IJwtService jwtService, string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(jwtService.Configuration["Jwt:Token"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken? validatedToken);

            JwtSecurityToken? jwtToken = (JwtSecurityToken)validatedToken;

            int userId = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
            Role role = Enum.Parse<Role>(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!);

            context.Items["User"] = new User { Id = userId, Role = role };
        }
        catch
        {
            // do nothing
        }
    }
}
