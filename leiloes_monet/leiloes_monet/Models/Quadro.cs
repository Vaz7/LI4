using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Quadro
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public int ano { get; set; }
        public double altura { get; set; }
        public double largura { get; set; }
        public string descricao { get; set; }
        public bool moldura { get; set; }
        public string autor {  get; set; }
        public Imagem imagem { get; set; }
    }
}
