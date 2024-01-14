using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace leiloes_monet.Models
{
    public class Imagem
    {
        public string NomeArquivo { get; set; }
		[Required(ErrorMessage = "Imagem é obrigatória")]
		public IFormFile ImageFile { get; set; }
    }


}
