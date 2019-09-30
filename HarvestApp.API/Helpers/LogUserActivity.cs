using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HarvestApp.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace HarvestApp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();
            var isCurrentUser = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value) == userId;

            var user = await repo.GetUser(userId,isCurrentUser);
            user.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}