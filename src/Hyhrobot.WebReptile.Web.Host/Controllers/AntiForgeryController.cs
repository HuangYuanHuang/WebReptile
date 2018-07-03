using Microsoft.AspNetCore.Antiforgery;
using Hyhrobot.WebReptile.Controllers;

namespace Hyhrobot.WebReptile.Web.Host.Controllers
{
    public class AntiForgeryController : WebReptileControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
