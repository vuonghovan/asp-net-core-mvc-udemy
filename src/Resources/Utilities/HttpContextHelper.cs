using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Utilities
{
    public static class HttpContextHelper
    {
        public static string ShowURL(this IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                var request = httpContextAccessor.HttpContext.Request;
                var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());

                return absoluteUri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string showPathBase(this IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                var request = httpContextAccessor.HttpContext.Request;
                var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent());

                return absoluteUri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GenerateUrlPath(this IHttpContextAccessor httpContextAccessor, string path)
        {
            try
            {
                var request = httpContextAccessor.HttpContext.Request;
                var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        path);

                return absoluteUri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get value in claim collection on Token by name
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static long GetValueFromToken(this IHttpContextAccessor httpContext, string key = "jti")
        {
            try
            {
                if (httpContext == null)
                    throw new Exception("HttpContext is null!");
                var temp = httpContext.HttpContext.User.FindFirstValue(key);
                return Convert.ToInt64(temp);
            }
            catch
            {
                throw new Exception("Token invalid!");
            }
        }
    }
}
