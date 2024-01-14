namespace leiloes_monet.Models.DAL
{
    public interface ILeilao
    {
        public void addLeilao(Leilao leilao);
		public List<Leilao> getAll();
        public List<Leilao> getAllUser(String email);
        public List<Leilao> getAllLicitados(String email);
        public Leilao GetLeilaoById(int leilaoId);
        public bool addLicitacao(Licitacao l);
    }
}
