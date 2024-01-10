using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonetLeiloesWeb.Models
{
    public class Leilao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string nome { get; set; }
        [Required]
        public DateTime data_inicio { get; set; }
        [Required]
        public DateTime data_fim { get; set; }
        [Required]
        public double valor_base { get; set; }
        [Required]
        [StringLength(100)]
        public string emailUtilizador { get; set; }
        [ForeignKey("emailUtilizador")]
        public virtual Utilizador utilizador { get; set; }
        public virtual ICollection<Licitacao> Licitacoes { get; set; }
    }
}
