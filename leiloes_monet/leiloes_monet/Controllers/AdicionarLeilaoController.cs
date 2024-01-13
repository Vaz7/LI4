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
            return View();
        }

        [HttpPost]
        public IActionResult addLeilao(Leilao leilao)
        {
            if (HttpContext.Session.GetString("Autorizado") == "ok")
            {
                Utilizador user = iuser.getUser(HttpContext.Session.GetString("email"));


                string fileName = caminhoServidor + "\\imagem\\";
                string novoNomeParaImagem = Guid.NewGuid().ToString() + "_" + leilao.quadro.imagem.ImageFile;

                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }

                using (var stream = System.IO.File.Create(fileName + novoNomeParaImagem))
                {
                    leilao.quadro.imagem.ImageFile.CopyToAsync(stream); // copia os dados para o arquivo
                }
                leilao.quadro.imagem.NomeArquivo = fileName + novoNomeParaImagem;
                leilao.utilizador = user;
                leilao.data_inicio = DateTime.Now;
                leilao.data_fim = leilao.data_inicio.AddDays(7);
                leilao.estado = false;


                if (!ModelState.IsValid)
                {
                    ileilao.addLeilao(leilao);

                    TempData["Criado"] = "Leilão Criado com Sucesso!";
                    return RedirectToAction("Index", "HomeLogged");
                }
            }
            
            return View(leilao);
        }
    }
}
