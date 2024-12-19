using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesignProject1Web.Controllers
{
    [Authorize]
    public class ConversorController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
