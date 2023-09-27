using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.Utils
{
    public interface IUserContext
    {
        string CurrentUserId { get; }
        string UserRole { get; }
    }
    public class UserContext : IUserContext
    {
        public string CurrentUserId { get; }
        public string UserRole { get; }

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            //CurrentUserId = contextAccessor.HttpContext.User.Identity.GetUserId();
            //UserRole = contextAccessor.HttpContext.User.Identity.Role
            //_httpContextAccessor = contextAccessor;
            //CurrentUserId = User.Identity.GetUserId();
            //CurrentUserId = GetClaimValue<long>("UserId", contextAccessor.HttpContext);
            //UserRole = GetClaimValue<string>("Role", contextAccessor.HttpContext);
        }

        private static T? GetClaimValue<T>(string claimName, HttpContext? context)
        {
            //var currentSessionUserEmail = _httpContextAccessor.HttpContext.User.Identity.Name;

            //var CurrentUserId = User.Identity.GetUserId();
            var userClaims = context?.User.Claims.ToArray<Claim>();
            if (userClaims == null || userClaims.Length == 0)
                return default;
            var value = userClaims.FirstOrDefault(x => x.Type == claimName)?.Value;

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
