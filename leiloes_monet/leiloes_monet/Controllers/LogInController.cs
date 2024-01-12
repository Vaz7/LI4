using leiloes_monet.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace leiloes_monet.Controllers
{
	public class LogInController : Controller
	{
        private readonly IUser iuser;
        public LogInController(IUser iuser)
        {
            this.iuser = iuser;
        }
        public IActionResult Login()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Login(String email, String password)
		{
			bool exists = iuser.UserExists(email,password);
			if (!exists)
			{
				TempData["AccountError"] = "Invalid Account!";
				return View();
			}
			else
			{
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("Autorizado", "ok");
                return RedirectToAction("Index", "HomeLogged");
            }
		}
	}
}
