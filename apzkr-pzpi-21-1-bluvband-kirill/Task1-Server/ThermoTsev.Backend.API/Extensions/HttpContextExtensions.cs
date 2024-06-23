using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.API.Extensions;

public static class HttpContextExtensions
{
    public static bool HasAdminRole(this HttpContext httpContext)
    {
        User? identity = httpContext.Items["User"] as User;
        return identity?.Role == Role.Admin;
    }
}
