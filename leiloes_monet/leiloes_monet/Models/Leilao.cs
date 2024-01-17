using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Leilao
    {
        public int idLeilao { get; set; }
		[Required(ErrorMessage = "Nome é obrigatório")]
		public string nome { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public bool estado { get; set; }
		[Required(ErrorMessage = "Valor base é obrigatório")]
        [Range(0, 9999999.99, ErrorMessage = "The valor field must be a valid decimal with a maximum of 7 digits and 2 decimal places.")]
        public double valor_base { get; set; }
        public bool pago { get; set; }
        public Utilizador utilizador { get; set; }
        public Quadro quadro { get; set; }
        public List<Licitacao>? licitacoes;//o ? quer dizer que pode ser null
    }
}
