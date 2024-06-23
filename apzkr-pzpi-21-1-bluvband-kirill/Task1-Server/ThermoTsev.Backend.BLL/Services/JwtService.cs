using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public IConfiguration Configuration { get; set; } = configuration;

    public string GenerateToken(int userId, Role role)
    {
        List<Claim> tokenClaims =
        [
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        ];

        SymmetricSecurityKey jwtEncKey = new(
            Encoding.UTF8.GetBytes(
                Configuration.GetSection("Jwt:Token")
                    .Value!
            )
        );
        SigningCredentials signingCredentials = new(jwtEncKey, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken token = new(
            claims: tokenClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        string? jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
