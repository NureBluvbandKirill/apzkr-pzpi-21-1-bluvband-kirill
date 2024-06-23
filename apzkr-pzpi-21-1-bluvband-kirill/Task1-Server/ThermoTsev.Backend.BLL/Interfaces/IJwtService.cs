using Microsoft.Extensions.Configuration;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IJwtService
{
    public IConfiguration Configuration { get; set; }

    public string GenerateToken(int userId, Role role);
}
