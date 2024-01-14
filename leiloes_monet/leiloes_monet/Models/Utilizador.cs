using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
	public class Utilizador
	{
		[Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid email address")]
		public string email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string nome { get; set; }

		[Required(ErrorMessage = "Date of birth is required")]
		public DateTime data_nascimento { get; set; }

		[Required(ErrorMessage = "NIF is required"), StringLength(9, MinimumLength = 9, ErrorMessage = "NIF must have 9 digits")]
		public string nif { get; set; }

		[Required(ErrorMessage = "Password is required"), StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
		public string password { get; set; }

		public Morada morada { get; set; }
	}
}
