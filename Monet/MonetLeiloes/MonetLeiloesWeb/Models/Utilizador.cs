using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonetLeiloesWeb.Models
{
    public class Utilizador
    {
        [StringLength(100)]
        public string email { get; set; }
        [Required]
        [StringLength(20)]
        public string password { get; set; }
        [Required]
        [StringLength(45)]
        public string nome { get; set; }
        [Required]
        public DateTime data_nascimento { get; set; }
        [Required]
        [StringLength(9)]
        public string nif { get; set; }
        [Required]
        public int idMorada { get; set; }
        [ForeignKey("idMorada")]
        public virtual Morada morada { get; set; }
    }
}
