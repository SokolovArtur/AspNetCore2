using Microsoft.AspNetCore.Mvc;

namespace Tochka.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "User", new { area = "Accounts" });
        }
    }
}
