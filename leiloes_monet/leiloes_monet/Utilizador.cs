namespace leiloes_monet.Pages
{
    [Serializable]
    public class Utilizador
    {
        public String email { get; set; }
        public String password { get; set; }
        public String nome { get; set; }
        public DateOnly data_nascimento { get; set; }
        public String nif { get; set; }
        //public Morada morada { get; set; }

    }

}
