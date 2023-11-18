namespace leiloes_monet.Pages
{
    [Serializable]
    public class Utilizador
    {
        public String email { get; set; }
        public String password { get; set; }
        public String nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public String nif { get; set; }
        public Morada morada { get; set; }

        public Utilizador(String email, String nome, DateTime data, String nif, String password, Morada morada)
        {
            this.email = email;
            this.password = password;
            this.nome = nome;
            this.data_nascimento = data;
            this.nif = nif;
            this.morada = morada;
        }
        public Utilizador(String nome)
        {
            this.nome = nome;
        }
        public Utilizador(String userName, String eMail, String password)
        {
            this.nome = userName;
            this.email = eMail;
            this.password = password;
        }

        public override string ToString()
        {
            return $"User:\nEmail: {email}, Password: {password}, Nome: {nome}, " +
                   $"Data Nascimento: {data_nascimento}, NIF: {nif}, Morada: {morada}\n";
        }
    }

}
