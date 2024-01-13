using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace leiloes_monet.Models
{
    public class Imagem
    {
        public string NomeArquivo { get; set; }
        public IFormFile ImageFile { get; set; }
    }


}
