using Microsoft.AspNetCore.Mvc;

namespace DesignProject1Web.Controllers
{
    public class CurrencyController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
