using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SP.Contract.Common.Extensions
{
    public static class HttpContextHelper
    {
        public static HttpContext BuildContext(long accountId, string login, string jwt)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authorization", $"Bearer {jwt}");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login, accountId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, accountId.ToString(), ClaimValueTypes.String),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(";", new string[] { }))
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Bearer",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            httpContext.User = new ClaimsPrincipal(claimsIdentity);

            return httpContext;
        }
    }
}
