using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Make sure to include this namespace for HttpContext

namespace leiloes_monet.Controllers
{
    public class HomeLoggedController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("email", "notOk");
            HttpContext.Session.SetString("Autorizado", "notOk");
            return RedirectToAction("Index", "Home");
        }
    }
}
