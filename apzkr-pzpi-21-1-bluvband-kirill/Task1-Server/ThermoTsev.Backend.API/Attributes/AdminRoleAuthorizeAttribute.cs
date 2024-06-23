using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ThermoTsev.Backend.API.Extensions;

namespace ThermoTsev.Backend.API.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AdminRoleAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.HasAdminRole())
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            return;
        }

        base.OnActionExecuting(context);
    }
}
