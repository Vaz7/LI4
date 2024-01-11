using Microsoft.AspNetCore.Mvc;
using MonetLeiloesWeb.Data;
using MonetLeiloesWeb.Models;

namespace leiloes_monet.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SignUpController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Utilizador obj)
        {

            if (_db.Utilizador.Any(u => u.email == obj.email))
            {
                ModelState.AddModelError("obj.email", "Email already exists");
                return View(obj);
            }

            //o problema é a colection de licitaçoes
            if (ModelState.IsValid)
            {
                Morada morada = new Morada
                {
                    cidade = obj.morada.cidade,
                    cod_postal = obj.morada.cod_postal,
                    pais = obj.morada.pais,
                    rua = obj.morada.rua
                };

                obj.morada = morada;

                _db.Utilizador.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index","Home");
            }
            return View(obj);
        }
    }
}
