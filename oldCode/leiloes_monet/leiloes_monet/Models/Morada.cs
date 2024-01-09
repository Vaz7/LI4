namespace leiloes_monet
{
    [Serializable]
    public class Morada
    {
        public int id { get; set; }
        public String rua { get; set; }
        public String cidade { get; set; }
        public String cod_postal { get; set; }
        public String pais { get; set; }

        public Morada(int id, String rua, String cidade, String cod_postal, String pais) {
            this.id = id;
            this.rua = rua;
            this.cidade = cidade;
            this.cod_postal = cod_postal;
            this.pais = pais;
        }

        public override string ToString()
        {
            return $"Morada:\nMorada ID: {id}, Rua: {rua}, Cidade: {cidade}, " +
                   $"Código Postal: {cod_postal}, País: {pais}\n";
        }
    }
}
