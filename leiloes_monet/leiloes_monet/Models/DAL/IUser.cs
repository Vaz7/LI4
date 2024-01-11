namespace leiloes_monet.Models.DAL
{
    public interface IUser
    {
        public void addClient(Utilizador user);
        public bool EmailExists(String email);
    }
}
