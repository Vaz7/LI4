namespace leiloes_monet.Models.DAL
{
    public interface IUser
    {
        public void addClient(Utilizador user);
        public bool EmailExists(String email);
        public bool UserExists(String email, String password);
        public Utilizador getUser(String email);
		public bool updateClient(Utilizador user);

	}
}
