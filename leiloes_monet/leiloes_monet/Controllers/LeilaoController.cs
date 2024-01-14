using leiloes_monet.Models.DAL;
using leiloes_monet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace leiloes_monet.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly ILeilao ileilao;

        public LeilaoController(ILeilao ileilao)
        {
            this.ileilao = ileilao;
        }

        [HttpGet]
        public IActionResult Index(int leilaoId)
        {
            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {
                Leilao leilao = ileilao.GetLeilaoById(leilaoId);
                return View(leilao);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

		public IActionResult AddLicitacao(int leilaoId, double licitacao)
        {
			if (HttpContext.Session.GetString("Autorizado") == "ok")
			{
				Leilao l = ileilao.GetLeilaoById(leilaoId);
				if (!l.licitacoes.IsNullOrEmpty())
				{
					if (licitacao > l.licitacoes.Last().valor)
					{
						Licitacao lic = new Licitacao()
						{
							data = DateTime.Now,
							valor = licitacao,
							idLeilao = leilaoId,
							emailUtilizador = HttpContext.Session.GetString("email")
						};
						ileilao.addLicitacao(lic);
						l.licitacoes.Add(lic);
						TempData["Licitado"] = "Licitação registada!";
						return View("Index",l);
					}
					else
					{
						TempData["Licitadoinv"] = "Licitação inválida!";
						return View("Index",l);
					}
					
				}
				else
				{
					if (licitacao > l.valor_base)
					{
						Licitacao lic = new Licitacao()
						{
							data = DateTime.Now,
							valor = licitacao,
							idLeilao = leilaoId,
							emailUtilizador = HttpContext.Session.GetString("email")
						};
						ileilao.addLicitacao(lic);
						l.licitacoes.Add(lic);
						TempData["Licitado"] = "Licitação registada!";
						return View("Index", l);
					}
					else
					{
						TempData["Licitadoinv"] = "Licitação inválida!";
						return View("Index", l);
					}


				}
			}
			else
				return RedirectToAction("Index", "Home");
			

		}
    }
}


