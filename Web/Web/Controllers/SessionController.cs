using Logic.Service;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Login()
        {
            string hostUrl = Request.Host.ToString();

            string loginUrl = SteamService.GetLoginUrl(hostUrl).Result;

            return Redirect(loginUrl);
        }
    }
}
