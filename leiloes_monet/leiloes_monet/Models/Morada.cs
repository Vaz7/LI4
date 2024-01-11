using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace leiloes_monet.Models
{
    public class Morada
    {
        public int Id { get; set; }
        public string rua { get; set; }
        public string cidade { get; set; }
        public string cod_postal { get; set;}
        public string pais { get; set; }
    }
}
