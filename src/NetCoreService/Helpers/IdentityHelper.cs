using Infrastructure.LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.Identity;
using System;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public static class IdentityHelper
    {
        private readonly static ILoggerManager _loggerManager;

        public static async Task<AppUser> GetUserLogging(this UserManager<AppUser> userManager, HttpContext httpContext)
        {
            try
            {
                var userIdLogging = userManager.GetUserId(httpContext.User);
                return await userManager.FindByIdAsync(userIdLogging);
            }
            catch
            {
                return null;
            }
        }

        public static Task<long> GetUserIdLogging(this UserManager<AppUser> userManager, HttpContext httpContext)
        {
            try
            {
                return Task.FromResult(long.Parse(userManager.GetUserId(httpContext.User)));
            }
            catch(Exception ex)
            {
                // TODO: Need re-check it
                _loggerManager.LogError(ex);
                return Task.FromResult((long)0);
            }
        }
    }
}
