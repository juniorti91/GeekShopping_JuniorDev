using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() => RedirectToAction(nameof(Index)));
        }
    }
}