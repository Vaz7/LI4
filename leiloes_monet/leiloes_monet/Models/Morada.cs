using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace leiloes_monet.Models
{
	public class Morada
	{
		[Required(ErrorMessage = "Rua é obrigatória")]
		public string rua { get; set; }

		[Required(ErrorMessage = "Cidade é obrigatória")]
		public string cidade { get; set; }

		[Required(ErrorMessage = "Código postal é obrigatório")]
		[RegularExpression(@"^\d{4}-\d{3}$", ErrorMessage = "Código-postal deve estar no formato XXXX-XXX")]
		public string cod_postal { get; set; }

		[Required(ErrorMessage = "País é obrigatório")]
		public string pais { get; set; }
	}

}
