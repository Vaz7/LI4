using System.Collections.Generic;
using System.Data.SqlClient;

namespace leiloes_monet.Models.DAL
{
    public class LeilaoDAL : ILeilao
    {
        string connectionstring = DALConfig.connectionstring;

        public void addLeilao(Leilao leilao)
        {
            using (SqlConnection con = new SqlConnection(connectionstring)) 
            {
                con.Open();

                string quadroQuery = @"
                    INSERT INTO [Li4].[Quadro] ([titulo], [ano], [altura], [largura], [descricao], [moldura], [autor], [imagem])
                    VALUES (@titulo, @ano, @altura, @largura, @descricao, @moldura, @autor, @imagem);

                    SELECT SCOPE_IDENTITY() AS idQuadro;";

                int idQuadro;

                using (SqlCommand quadroCmd = new SqlCommand(quadroQuery, con))
                {
                    // Set Quadro parameters
                    quadroCmd.Parameters.AddWithValue("@titulo", leilao.quadro.titulo);
                    quadroCmd.Parameters.AddWithValue("@ano", leilao.quadro.ano);
                    quadroCmd.Parameters.AddWithValue("@altura", leilao.quadro.altura);
                    quadroCmd.Parameters.AddWithValue("@largura", leilao.quadro.largura);
                    quadroCmd.Parameters.AddWithValue("@descricao", leilao.quadro.descricao);
                    quadroCmd.Parameters.AddWithValue("@moldura", leilao.quadro.moldura);
                    quadroCmd.Parameters.AddWithValue("@autor", leilao.quadro.autor);
                    quadroCmd.Parameters.AddWithValue("@imagem", leilao.quadro.imagem.NomeArquivo);

                    // Execute the query and get the generated identity value
                    idQuadro = Convert.ToInt32(quadroCmd.ExecuteScalar());
                }

                string leilaoQuery = @"
                    INSERT INTO Li4.leilao (nome, data_inicio, data_fim, estado, valor_base, Utilizador_email, idQuadro)
                    VALUES (@nome, @dataInicio, @dataFim, @estado, @valorBase, @utilizadorEmail, @idQuadro);

                    SELECT SCOPE_IDENTITY() AS idLeilao;";

                using (SqlCommand leilaoCmd = new SqlCommand(leilaoQuery, con))
                {
                    // Set Leilao parameters
                    leilaoCmd.Parameters.AddWithValue("@nome", leilao.nome);
                    leilaoCmd.Parameters.AddWithValue("@dataInicio", leilao.data_inicio);
                    leilaoCmd.Parameters.AddWithValue("@dataFim", leilao.data_fim);
                    leilaoCmd.Parameters.AddWithValue("@estado", leilao.estado);
                    leilaoCmd.Parameters.AddWithValue("@valorBase", leilao.valor_base);
                    leilaoCmd.Parameters.AddWithValue("@utilizadorEmail", leilao.utilizador.email);
                    leilaoCmd.Parameters.AddWithValue("@idQuadro", idQuadro);

                    leilaoCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}
