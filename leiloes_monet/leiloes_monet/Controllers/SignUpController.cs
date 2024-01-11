using Microsoft.AspNetCore.Mvc;
using leiloes_monet.Models.DAL;
using leiloes_monet.Models;

namespace leiloes_monet.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUser iuser;

        public SignUpController(IUser iuser)
        {
            this.iuser = iuser;
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
            if (EmailExists(obj.email))
            {
                // Add a model error for the existing email
                ModelState.AddModelError("email", "Email already exists. Please choose a different one.");

                // Return the view with validation errors
                return View(obj);
            }
            if (!DateValid(obj.data_nascimento))
            {
                // Add a model error for the existing email
                ModelState.AddModelError("data_nascimento", "Date is not valid. Please choose a different one.");

                // Return the view with validation errors
                return View(obj);
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
                iuser.addClient(obj);


                return RedirectToAction("Index","Home");
            }
            return View(obj);
        }

        private bool EmailExists(string email)
        {
            // Use your IUser or other data access method to check if the email exists
            // You should implement this method based on your data access layer
            return iuser.EmailExists(email);
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
