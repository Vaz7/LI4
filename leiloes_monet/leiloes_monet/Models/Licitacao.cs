using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Licitacao
    {
        public string emailUtilizador { get; set; }
        public virtual Utilizador utilizador { get; set; }
        public int idLeilao { get; set; }
        public virtual Leilao leilao { get; set; }
        public DateTime data {  get; set; }
        public double valor {  get; set; }

    }
}
