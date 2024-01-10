using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonetLeiloesWeb.Models
{
    public class Quadro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        [Required]
        public int ano { get; set; }
        [Required]
        public double altura { get; set; }
        [Required]
        public double largura { get; set; }
        [Required]
        [StringLength(1000)]
        public string descricao { get; set; }
        [Required]
        public bool moldura { get; set; }
        [Required]
        [StringLength(100)]
        public string autor {  get; set; }
        [Required]
        public byte[] fotografia { get; set; }
        [Required]
        public int idLeilao { get; set; }
        [ForeignKey("idLeilao")]
        public virtual Leilao leilao { get; set; }
    }
}
