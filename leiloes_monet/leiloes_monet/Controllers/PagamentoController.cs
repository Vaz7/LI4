using leiloes_monet.Models.DAL;
using leiloes_monet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace leiloes_monet.Controllers
{
	public class PagamentoController : Controller
	{
        private readonly ILeilao ileilao;
		private readonly IUser iuser;

		public PagamentoController(ILeilao ileilao, IUser iuser)
        {
            this.ileilao = ileilao;
			this.iuser = iuser;
		}

        [HttpGet]
		public IActionResult Pagamento()
		{
			if (HttpContext.Session.GetString("Autorizado") == "ok")
			{
				Utilizador user = iuser.getUser(HttpContext.Session.GetString("email"));
				List<Leilao> leiloes = ileilao.getLeiloesGanhosNaoPagos(user.email);
                return View(leiloes);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public IActionResult PagarLeilao(int leilaoId) 
		{
            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {
                Leilao leilao = ileilao.GetLeilaoById(leilaoId);
                leilao.pago = true;
                ileilao.UpdateLeilaoPago(leilao.idLeilao);
                TempData["Pago"] = "Pagamento Efetuado!";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
	}
}
