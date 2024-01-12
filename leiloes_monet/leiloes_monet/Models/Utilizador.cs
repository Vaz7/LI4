using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
	public class Utilizador
	{
		[Required(ErrorMessage = "Email is required")]
		public string email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string nome { get; set; }

		[Required(ErrorMessage = "Date of birth is required")]
		public DateTime data_nascimento { get; set; }

		[Required(ErrorMessage = "NIF is required")]
		public string nif { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string password { get; set; }

		public Morada morada { get; set; }
	}
}
