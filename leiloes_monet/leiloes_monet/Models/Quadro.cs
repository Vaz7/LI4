using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Quadro
    {
		[Required(ErrorMessage = "Título é obrigatório")]
		public string titulo { get; set; }
        public int ano { get; set; }
		[Required(ErrorMessage = "Altura é obrigatória")]
		[Range(0, double.MaxValue, ErrorMessage = "Altura inválida")]
		public double altura { get; set; }
		[Required(ErrorMessage = "Largura é obrigatória")]
		[Range(0, double.MaxValue, ErrorMessage = "Largura inválida")]
		public double largura { get; set; }
		[Required(ErrorMessage = "Descrição é obrigatória")]
		public string descricao { get; set; }
        public bool moldura { get; set; }
		[Required(ErrorMessage = "Autor é obrigatório")]
		public string autor {  get; set; }
        public Imagem imagem { get; set; }
    }
}
