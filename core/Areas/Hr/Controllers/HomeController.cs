using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Vacancy", new { area = "Hr" });
        }
    }
}
