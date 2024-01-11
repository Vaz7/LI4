using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonetLeiloesWeb.Models
{
    public class Licitacao
    {
        [StringLength(100)]
        public string emailUtilizador { get; set; }
        [ForeignKey("emailUtilizador")]
        public virtual Utilizador utilizador { get; set; }
        public int idLeilao { get; set; }
        [ForeignKey("idLeilao")]
        public virtual Leilao leilao { get; set; }
        [Required]
        public DateTime data {  get; set; }
        [Required]
        public double valor {  get; set; }

    }
}
