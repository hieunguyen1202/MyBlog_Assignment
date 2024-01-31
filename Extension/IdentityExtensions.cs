using System.Linq;
using System.Security.Claims;

namespace MyBlog.Extension
{
    public static class IdentityExtensions
    {
        public static string GetAccountId(this System.Security.Principal.IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AccountId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetRoleId(this System.Security.Principal.IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RoleId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetVipCredits(this System.Security.Principal.IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("VipCredits");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetAvatar(this System.Security.Principal.IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
