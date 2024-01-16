using leiloes_monet.Models;
using leiloes_monet.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace leiloes_monet.Controllers
{
    public class AdicionarLeilaoController : Controller
    {
        private readonly ILeilao ileilao;
        private string caminhoServidor;
        private readonly IUser iuser;

        public AdicionarLeilaoController(IWebHostEnvironment sistema, ILeilao ileilao, IUser iuser)
        {
            this.caminhoServidor = sistema.WebRootPath;
            this.ileilao = ileilao;
            this.iuser = iuser;
        }

        public IActionResult addLeilao()
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

        [HttpPost]
        public IActionResult addLeilao(Leilao leilao)
        {
            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {
                Utilizador user = iuser.getUser(HttpContext.Session.GetString("email"));


                string fileName ="/imagem/";
                string filePath = caminhoServidor + "\\imagem\\";
                string novoNomeParaImagem = Guid.NewGuid().ToString() + "_" + leilao.quadro.imagem.ImageFile.FileName;

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var stream = System.IO.File.Create(filePath + novoNomeParaImagem))
                {
                    leilao.quadro.imagem.ImageFile.CopyTo(stream); // copia os dados para o arquivo
                }




                leilao.quadro.imagem.NomeArquivo = fileName + novoNomeParaImagem;
                leilao.utilizador = user;
                leilao.data_inicio = DateTime.Now;
                leilao.data_fim = leilao.data_inicio.AddMinutes(1);
                leilao.estado = false;
                leilao.pago = false;

                ileilao.addLeilao(leilao);

                TempData["Criado"] = "Leilão Criado com Sucesso!";
                return RedirectToAction("Index", "HomeLogged");
                
            }
            
            return View(leilao);
        }
    }
}
