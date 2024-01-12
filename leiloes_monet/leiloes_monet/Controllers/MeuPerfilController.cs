using leiloes_monet.Models.DAL;
using leiloes_monet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace leiloes_monet.Controllers
{
    public class MeuPerfilController : Controller
    {
		private readonly IUser iuser;
		public MeuPerfilController(IUser iuser)
		{
			this.iuser = iuser;
		}
		public IActionResult Meuperfil()
        {					
			if (HttpContext.Session.GetString("Autorizado") == "ok")
			{
				Utilizador user = iuser.getUser(HttpContext.Session.GetString("email"));
				return View(user);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(Utilizador obj)
		{
			if (!DateValid(obj.data_nascimento))
			{
				// Add a model error for the existing email
				ModelState.AddModelError("data_nascimento", "Date is not valid.");

				// Return the view with validation errors
				return RedirectToAction("Meuperfil","MeuPerfil");
			}

			if (ModelState.IsValid)
			{
				Morada morada = new Morada
				{
					cidade = obj.morada.cidade,
					cod_postal = obj.morada.cod_postal,
					pais = obj.morada.pais,
					rua = obj.morada.rua
				};
				iuser.updateClient(obj);

				TempData["Atualizado"] = "Atualização efetuada!";
				return RedirectToAction("Meuperfil", "MeuPerfil");
			}
			return RedirectToAction("Meuperfil", "MeuPerfil");
		}

		private bool DateValid(DateTime date)
		{
			DateTime minSqlDateTime = new DateTime(1753, 1, 1);
			DateTime maxSqlDateTime = new DateTime(9999, 12, 31, 23, 59, 59, 997);

			// Check if the date is within the valid SQL Server range
			if (date >= minSqlDateTime && date <= maxSqlDateTime)
			{
				// Check if the parsed date is equal to the original date, indicating a valid date
				string originalDateString = date.ToString("yyyy-MM-dd");
				if (DateTime.TryParse(originalDateString, out DateTime parsedDate))
				{
					return parsedDate.Date == date.Date;
				}
			}

			return false;
		}
	}
}
