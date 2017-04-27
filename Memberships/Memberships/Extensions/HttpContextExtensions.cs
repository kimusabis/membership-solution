using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace Memberships.Extensions
{
    public static class HttpContextExtensions
    {
        private const string nameidentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static string GetUserId(this HttpContext ctx)
        { 
            string uid=string.Empty;
            try {
                var claims = ctx.GetOwinContext().Get<ApplicationSignInManager>().AuthenticationManager
                            .User.Claims.FirstOrDefault(claim => claim.Type.Equals(nameidentifier));

                // check that the user is logged in and a claim exits
                if (claims != default(Claim)) {
                    uid = claims.Value;
                }
            } catch { }

            return uid;
        }
    }
}