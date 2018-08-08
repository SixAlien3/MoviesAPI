using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace MoviesAPI.Extensions
{
    public static class IdentityHelpers
    {
        public static string GetCurrentUserName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null && claimsIdentity.Claims.FirstOrDefault(x => x.Type == "userName") != null)
            {
                return claimsIdentity.Claims.FirstOrDefault(x => x.Type == "userName")?.Value;
            }

            return "0";
        }
    }
}