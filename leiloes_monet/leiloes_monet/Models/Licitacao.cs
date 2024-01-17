using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Licitacao
    {
        public string emailUtilizador { get; set; }
        public int idLeilao { get; set; }
        public DateTime data {  get; set; }
        [Range(0, 99999999999.99, ErrorMessage = "The valor field must be a valid decimal with a maximum of 12 digits and 2 decimal places.")]
        public double valor {  get; set; }

    }
}
