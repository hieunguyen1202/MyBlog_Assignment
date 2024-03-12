using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.Text;

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
        public static  bool VerifyEmail(string emailVerify)
        {
            using (WebClient webclient = new WebClient())
            {
                string url = "http://verify-email.org/";
                NameValueCollection formData = new NameValueCollection();
                formData["check"] = emailVerify;
                byte[] responseBytes = webclient.UploadValues(url, "POST", formData);
                string response = Encoding.ASCII.GetString(responseBytes);
                if (response.Contains("Result: Ok"))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
