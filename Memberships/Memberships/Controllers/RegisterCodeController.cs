using Memberships.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Controllers
{
    public class RegisterCodeController : Controller
    {
        // GET: RegisterCode
        public async Task<ActionResult> Register(string code)
        {
            if (Request.IsAuthenticated) {
                // fetch the user id
                var userId = HttpContext.User.Identity.GetUserId();

                //register a code with a user
                var registered = await SubscriptionExtensions.RegisterUserSubscriptionCode(code, userId);

                if (!registered) throw new ApplicationException();

                return PartialView("_RegisterCodePartial");
            }

            return View();
        }
    }
}