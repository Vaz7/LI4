using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonetLeiloesWeb.Models
{
    public class Morada
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string rua { get; set; }
        [Required]
        [StringLength(50)]
        public string cidade { get; set; }
        [Required]
        [StringLength(10)]
        public string cod_postal { get; set;}
        [Required]
        [StringLength(45)]
        public string pais { get; set; }
    }
}
