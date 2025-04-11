using APITemplate.DataAccess.Concrete.Context;
using APITemplate.Tools.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Reflection;

namespace APITemplate.Business.Logger;

public class LogActivityActionFilter : IAsyncActionFilter
{
    private readonly APITemplateContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogActivityActionFilter(APITemplateContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var attribute = actionDescriptor?.MethodInfo.GetCustomAttribute<LogActivityAttribute>();

        if (attribute != null)
        {
            var result = await next();

            string userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonymous";
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            string controllerName = actionDescriptor!.ControllerName;
            string actionName = actionDescriptor.ActionName;

            string description = attribute.Description;
            if (string.IsNullOrEmpty(description))
            {
                description = $"{controllerName}/{actionName} API endpoint'i çağrıldı";
            }

            if (context.RouteData.Values.TryGetValue("id", out var idValue))
            {
                description += $" - ID: {idValue}";
            }

            var log = new UserActivityLog
            {
                UserName = userName,
                ActionType = attribute.ActionType,
                Description = description,
                CreatedAt = DateTime.Now,
                IpAddress = ip,
            };

            _dbContext.Set<UserActivityLog>().Add(log);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            await next();
        }
    }
}
