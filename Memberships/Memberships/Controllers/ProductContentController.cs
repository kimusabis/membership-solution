using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Memberships.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Memberships.Controllers
{
    [Authorize]
    public class ProductContentController : Controller
    {
        // GET: ProductContent
        public async Task<ActionResult> Index(int id)
        {
            var userId = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserId() : null;

            var sections = await SectionExtensions.GetProductSectionAsync(id, userId);

            return View(sections);
        }

        public async Task<ActionResult> Content(int productId, int itemId)
        {
            var model = await SectionExtensions.GetContentAsync(productId, itemId);

            return View("Content", model);
        }
    }    
}