using leiloes_monet.Models.DAL;
using leiloes_monet.Models;
using Microsoft.AspNetCore.Mvc;

namespace leiloes_monet.Controllers
{
	public class VerLeiloesController : Controller
	{
		private readonly ILeilao ileilao;

		public VerLeiloesController(ILeilao ileilao)
		{
			this.ileilao = ileilao;
		}

		[HttpGet]
		public IActionResult Index()
		{

			if (HttpContext.Session.GetString("Autorizado") == "ok")
			{

				List<Leilao> leiloes = ileilao.getAll();
                var sortedLeiloes = leiloes.OrderByDescending(leilao => leilao.data_inicio);
                return View(sortedLeiloes);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
        public IActionResult MeusLeiloes()
        {

            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {

                List<Leilao> leiloes = ileilao.getAllUser(HttpContext.Session.GetString("email"));
                var sortedLeiloes = leiloes.OrderByDescending(leilao => leilao.data_inicio);
                return View("Index", sortedLeiloes);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult LeiloesLicitados()
        {

            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {

                List<Leilao> leiloes = ileilao.getAllLicitados(HttpContext.Session.GetString("email"));
                var sortedLeiloes = leiloes.OrderByDescending(leilao => leilao.data_inicio);
                return View("Index", sortedLeiloes);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
