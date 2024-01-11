using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Utilizador
    {
        public string email { get; set; }
        public string password { get; set; }
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public string nif { get; set; }
        public int idMorada { get; set; }
        public virtual Morada morada { get; set; }
    }
}
