using Microsoft.AspNetCore.Mvc;

namespace leiloes_monet.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
