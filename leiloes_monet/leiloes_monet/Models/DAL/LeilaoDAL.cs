using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

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
                    INSERT INTO Li4.leilao (nome, data_inicio, data_fim, estado, valor_base, pago, Utilizador_email, idQuadro)
                    VALUES (@nome, @dataInicio, @dataFim, @estado, @valorBase, @pago, @utilizadorEmail, @idQuadro);

                    SELECT SCOPE_IDENTITY() AS idLeilao;";

                using (SqlCommand leilaoCmd = new SqlCommand(leilaoQuery, con))
                {
                    // Set Leilao parameters
                    leilaoCmd.Parameters.AddWithValue("@nome", leilao.nome);
                    leilaoCmd.Parameters.AddWithValue("@dataInicio", leilao.data_inicio);
                    leilaoCmd.Parameters.AddWithValue("@dataFim", leilao.data_fim);
                    leilaoCmd.Parameters.AddWithValue("@estado", leilao.estado);
                    leilaoCmd.Parameters.AddWithValue("@valorBase", leilao.valor_base);
					leilaoCmd.Parameters.AddWithValue("@pago", leilao.pago);
					leilaoCmd.Parameters.AddWithValue("@utilizadorEmail", leilao.utilizador.email);
                    leilaoCmd.Parameters.AddWithValue("@idQuadro", idQuadro);

                    leilaoCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public List<Leilao> getAll()
        {
            List<Leilao> leiloes = new List<Leilao>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

				// First, update the estado field
				string updateQuery = @"
            UPDATE Li4.leilao
            SET estado = CASE 
                            WHEN data_fim < GETDATE() THEN 1
                            ELSE 0
                         END
            WHERE data_fim > GETDATE() AND estado = 0";

				using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
				{
					updateCommand.ExecuteNonQuery();
				}

				// Query to get Leilao information
				string leilaoQuery = @"
            SELECT l.idLeilao, l.nome, l.data_inicio, l.data_fim, l.estado, l.valor_base, l.pago,
                   l.Utilizador_email, q.idQuadro, q.titulo, q.ano, q.altura, q.largura, q.descricao,
                   q.moldura, q.autor, q.imagem
            FROM Li4.leilao l
            JOIN Li4.Quadro q ON l.idQuadro = q.idQuadro";

                using (SqlCommand leilaoCmd = new SqlCommand(leilaoQuery, con))
                {
                    using (SqlDataReader leilaoReader = leilaoCmd.ExecuteReader())
                    {
                        while (leilaoReader.Read())
                        {
                            Leilao leilao = new Leilao
                            {
                                idLeilao = Convert.ToInt32(leilaoReader["idLeilao"]),
                                nome = leilaoReader["nome"].ToString(),
                                data_inicio = Convert.ToDateTime(leilaoReader["data_inicio"]),
                                data_fim = Convert.ToDateTime(leilaoReader["data_fim"]),
                                estado = Convert.ToBoolean(leilaoReader["estado"]),
                                valor_base = Convert.ToDouble(leilaoReader["valor_base"]),
                                pago = Convert.ToBoolean(leilaoReader["pago"]),
								utilizador = new Utilizador { email = leilaoReader["Utilizador_email"].ToString() },
                                quadro = new Quadro
                                {
                                    titulo = leilaoReader["titulo"].ToString(),
                                    ano = Convert.ToInt32(leilaoReader["ano"]),
                                    altura = Convert.ToDouble(leilaoReader["altura"]),
                                    largura = Convert.ToDouble(leilaoReader["largura"]),
                                    descricao = leilaoReader["descricao"].ToString(),
                                    moldura = Convert.ToBoolean(leilaoReader["moldura"]),
                                    autor = leilaoReader["autor"].ToString(),
                                    imagem = new Imagem { NomeArquivo = leilaoReader["imagem"].ToString() },
                                },
                                licitacoes = new List<Licitacao>()
                            };

                            if (leilao.estado == false)
                            {
                                leiloes.Add(leilao);
                            }
                        }
                    }
                }

                // Query to get Licitacao information
                string licitacaoQuery = @"
            SELECT idLicitacao, utilizador_email, leilao_idLeilao, data, valor
            FROM Li4.licitacao";

                using (SqlCommand licitacaoCmd = new SqlCommand(licitacaoQuery, con))
                {
                    using (SqlDataReader licitacaoReader = licitacaoCmd.ExecuteReader())
                    {
                        while (licitacaoReader.Read())
                        {
                            int leilaoId = Convert.ToInt32(licitacaoReader["leilao_idLeilao"]);

                            Leilao leilao = leiloes.FirstOrDefault(l => l.idLeilao == leilaoId);

                            if (leilao != null)
                            {
                                Licitacao licitacao = new Licitacao
                                {

                                    emailUtilizador = licitacaoReader["utilizador_email"].ToString(),
                                    idLeilao = leilaoId,
                                    data = Convert.ToDateTime(licitacaoReader["data"]),
                                    valor = Convert.ToDouble(licitacaoReader["valor"])

                                };

                                leilao.licitacoes.Add(licitacao);
                            }
                        }
                    }
                }
            }

            return leiloes;
        }


        public List<Leilao> getAllUser(string email)
        {
            List<Leilao> leiloes = new List<Leilao>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

				// First, update the estado field
				string updateQuery = @"
            UPDATE Li4.leilao
            SET estado = CASE 
                            WHEN data_fim < GETDATE() THEN 1
                            ELSE 0
                         END
            WHERE data_fim > GETDATE() AND estado = 0";

				using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
				{
					updateCommand.ExecuteNonQuery();
				}

				// Query to get Leilao information
				string leilaoQuery = @"
            SELECT l.idLeilao, l.nome, l.data_inicio, l.data_fim, l.estado, l.valor_base, l.pago,
                   l.Utilizador_email, q.idQuadro, q.titulo, q.ano, q.altura, q.largura, q.descricao,
                   q.moldura, q.autor, q.imagem
            FROM Li4.leilao l
            JOIN Li4.Quadro q ON l.idQuadro = q.idQuadro
            WHERE l.Utilizador_email = @UserEmail";

                using (SqlCommand leilaoCmd = new SqlCommand(leilaoQuery, con))
                {
                    leilaoCmd.Parameters.AddWithValue("@UserEmail", email);

                    using (SqlDataReader leilaoReader = leilaoCmd.ExecuteReader())
                    {
                        while (leilaoReader.Read())
                        {
                            Leilao leilao = new Leilao
                            {
                                idLeilao = Convert.ToInt32(leilaoReader["idLeilao"]),
                                nome = leilaoReader["nome"].ToString(),
                                data_inicio = Convert.ToDateTime(leilaoReader["data_inicio"]),
                                data_fim = Convert.ToDateTime(leilaoReader["data_fim"]),
                                estado = Convert.ToBoolean(leilaoReader["estado"]),
                                valor_base = Convert.ToDouble(leilaoReader["valor_base"]),
								pago = Convert.ToBoolean(leilaoReader["pago"]),
								utilizador = new Utilizador { email = leilaoReader["Utilizador_email"].ToString() },
                                quadro = new Quadro
                                {
                                    titulo = leilaoReader["titulo"].ToString(),
                                    ano = Convert.ToInt32(leilaoReader["ano"]),
                                    altura = Convert.ToDouble(leilaoReader["altura"]),
                                    largura = Convert.ToDouble(leilaoReader["largura"]),
                                    descricao = leilaoReader["descricao"].ToString(),
                                    moldura = Convert.ToBoolean(leilaoReader["moldura"]),
                                    autor = leilaoReader["autor"].ToString(),
                                    imagem = new Imagem { NomeArquivo = leilaoReader["imagem"].ToString() }
                                },
                                licitacoes = new List<Licitacao>()
                            };

                            leiloes.Add(leilao);
                        }
                    }
                }

                // Query to get Licitacao information
                string licitacaoQuery = @"
            SELECT idLicitacao, utilizador_email, leilao_idLeilao, data, valor
            FROM Li4.licitacao
            WHERE leilao_idLeilao IN (SELECT idLeilao FROM Li4.leilao WHERE Utilizador_email = @UserEmail)";

                using (SqlCommand licitacaoCmd = new SqlCommand(licitacaoQuery, con))
                {
                    licitacaoCmd.Parameters.AddWithValue("@UserEmail", email);

                    using (SqlDataReader licitacaoReader = licitacaoCmd.ExecuteReader())
                    {
                        while (licitacaoReader.Read())
                        {
                            int leilaoId = Convert.ToInt32(licitacaoReader["leilao_idLeilao"]);

                            Leilao leilao = leiloes.FirstOrDefault(l => l.idLeilao == leilaoId);

                            if (leilao != null)
                            {
                                Licitacao licitacao = new Licitacao
                                {
                                    emailUtilizador = licitacaoReader["utilizador_email"].ToString(),
                                    idLeilao = leilaoId,
                                    data = Convert.ToDateTime(licitacaoReader["data"]),
                                    valor = Convert.ToDouble(licitacaoReader["valor"])
                                };

                                leilao.licitacoes.Add(licitacao);
                            }
                        }
                    }
                }
            }

            return leiloes;
        }


        public List<Leilao> getAllLicitados(string email)
        {
            List<Leilao> leiloes = new List<Leilao>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

				// First, update the estado field
				string updateQuery = @"
            UPDATE Li4.leilao
            SET estado = CASE 
                            WHEN data_fim < GETDATE() THEN 1
                            ELSE 0
                         END
            WHERE data_fim > GETDATE() AND estado = 0";

				using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
				{
					updateCommand.ExecuteNonQuery();
				}

				// Query to get distinct leilao_idLeilao values for a specific user
				string query = "SELECT DISTINCT leilao_idLeilao FROM Li4.licitacao WHERE utilizador_email = @UtilizadorEmail";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UtilizadorEmail", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int leilaoId = reader.GetInt32(0);
                            Leilao leilao = GetLeilaoById(leilaoId);

                            if (leilao != null)
                            {
                                leiloes.Add(leilao);
                            }
                        }
                    }
                }
            }

            return leiloes;
        }


        public Leilao GetLeilaoById(int leilaoId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                // Query to get Leilao information by ID
                string leilaoQuery = @"
        SELECT l.idLeilao, l.nome, l.data_inicio, l.data_fim, l.estado, l.valor_base, l.pago,
               l.Utilizador_email, q.idQuadro, q.titulo, q.ano, q.altura, q.largura, q.descricao,
               q.moldura, q.autor, q.imagem, li.idLicitacao, li.utilizador_email AS licitacao_utilizador_email,
               li.leilao_idLeilao, li.data AS licitacao_data, li.valor AS licitacao_valor
        FROM Li4.leilao l
        JOIN Li4.Quadro q ON l.idQuadro = q.idQuadro
        LEFT JOIN Li4.licitacao li ON l.idLeilao = li.leilao_idLeilao
        WHERE l.idLeilao = @LeilaoId";

                using (SqlCommand leilaoCmd = new SqlCommand(leilaoQuery, con))
                {
                    leilaoCmd.Parameters.AddWithValue("@LeilaoId", leilaoId);

                    using (SqlDataReader leilaoReader = leilaoCmd.ExecuteReader())
                    {
                        Leilao leilao = null;

                        while (leilaoReader.Read())
                        {
                            if (leilao == null)
                            {
                                leilao = new Leilao
                                {
                                    idLeilao = Convert.ToInt32(leilaoReader["idLeilao"]),
                                    nome = leilaoReader["nome"].ToString(),
                                    data_inicio= Convert.ToDateTime(leilaoReader["data_inicio"]),
                                    data_fim = Convert.ToDateTime(leilaoReader["data_fim"]),
                                    estado = Convert.ToBoolean(leilaoReader["estado"]),
                                    valor_base= Convert.ToDouble(leilaoReader["valor_base"]),
									pago = Convert.ToBoolean(leilaoReader["pago"]),
									utilizador = new Utilizador { email = leilaoReader["Utilizador_email"].ToString() },
                                    quadro = new Quadro
                                    {
                                        titulo = leilaoReader["titulo"].ToString(),
                                        ano = Convert.ToInt32(leilaoReader["ano"]),
                                        altura = Convert.ToDouble(leilaoReader["altura"]),
                                        largura = Convert.ToDouble(leilaoReader["largura"]),
                                        descricao = leilaoReader["descricao"].ToString(),
                                        moldura = Convert.ToBoolean(leilaoReader["moldura"]),
                                        autor = leilaoReader["autor"].ToString(),
                                        imagem = new Imagem { NomeArquivo = leilaoReader["imagem"].ToString() },
                                    },
                                    licitacoes = new List<Licitacao>()
                                };
                            }

                            // Check if there are associated Licitacoes
                            if (leilaoReader["idLicitacao"] != DBNull.Value)
                            {
                                Licitacao licitacao = new Licitacao
                                {
                                    emailUtilizador = leilaoReader["licitacao_utilizador_email"].ToString(),
                                    idLeilao = Convert.ToInt32(leilaoReader["leilao_idLeilao"]),
                                    data = Convert.ToDateTime(leilaoReader["licitacao_data"]),
                                    valor= Convert.ToDouble(leilaoReader["licitacao_valor"])
                                };

                                leilao.licitacoes.Add(licitacao);
                            }
                        }
                        return leilao;
                    }
                }
            }

            return null;
        }


		public bool addLicitacao(Licitacao l)
		{
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				con.Open();

				// Query to add a new bid
				string query = @"
            INSERT INTO Li4.licitacao (utilizador_email, leilao_idLeilao, data, valor)
            VALUES (@utilizadorEmail, @leilaoId, @data, @valor)";

				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					// Set parameters for the bid
					cmd.Parameters.AddWithValue("@utilizadorEmail", l.emailUtilizador);
					cmd.Parameters.AddWithValue("@leilaoId", l.idLeilao);
					cmd.Parameters.AddWithValue("@data", l.data);
					cmd.Parameters.AddWithValue("@valor", l.valor);

					// Execute the query
					int rowsAffected = cmd.ExecuteNonQuery();

					// Check if the bid was successfully added
					return rowsAffected > 0;
				}
			}
		}


        public void UpdateLeilaoPago(int idLeilao)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                string updateLeilaoQuery = @"
            UPDATE Li4.leilao
            SET pago = 1
                    WHERE idLeilao = @idLeilao; ";
        
        using (SqlCommand updateLeilaoCmd = new SqlCommand(updateLeilaoQuery, con))
                {
                    updateLeilaoCmd.Parameters.AddWithValue("@idLeilao", idLeilao);
                    updateLeilaoCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }


		public List<Leilao> getLeiloesGanhosNaoPagos(string utilizadorId)
		{
			List<Leilao> activeLeiloes = new List<Leilao>();

			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				con.Open();

				string query = @"
            SELECT l.idLeilao, l.nome, l.data_inicio, l.data_fim, l.estado, l.valor_base, l.pago,
               l.Utilizador_email
            FROM Li4.leilao l
            INNER JOIN Li4.licitacao lic ON l.idLeilao = lic.leilao_idLeilao
            WHERE l.estado = 1 
              AND l.pago = 0
			  AND lic.Utilizador_email = @utilizadorEmail
			  AND lic.valor = (SELECT MAX(valor) FROM Li4.licitacao WHERE leilao_idLeilao = l.idLeilao); ";
		

		using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@utilizadorEmail", utilizadorId);

					using (SqlDataReader leilaoReader = cmd.ExecuteReader())
					{
						while (leilaoReader.Read())
						{
                            Leilao leilao = new Leilao
                            {
                                idLeilao = Convert.ToInt32(leilaoReader["idLeilao"]),
                                nome = leilaoReader["nome"].ToString(),
                                data_inicio = Convert.ToDateTime(leilaoReader["data_inicio"]),
                                data_fim = Convert.ToDateTime(leilaoReader["data_fim"]),
                                estado = Convert.ToBoolean(leilaoReader["estado"]),
                                valor_base = Convert.ToDouble(leilaoReader["valor_base"]),
                                pago = Convert.ToBoolean(leilaoReader["pago"]),
                                utilizador = new Utilizador { email = leilaoReader["Utilizador_email"].ToString() },
                                quadro = new Quadro
                                {
                                    titulo = leilaoReader["titulo"].ToString(),
                                    ano = Convert.ToInt32(leilaoReader["ano"]),
                                    altura = Convert.ToDouble(leilaoReader["altura"]),
                                    largura = Convert.ToDouble(leilaoReader["largura"]),
                                    descricao = leilaoReader["descricao"].ToString(),
                                    moldura = Convert.ToBoolean(leilaoReader["moldura"]),
                                    autor = leilaoReader["autor"].ToString(),
                                    imagem = new Imagem { NomeArquivo = leilaoReader["imagem"].ToString() }
                                },
                                licitacoes = new List<Licitacao>()
                            };

                            if (leilaoReader["idLicitacao"] != DBNull.Value)
                            {
                                Licitacao licitacao = new Licitacao
                                {
                                    emailUtilizador = leilaoReader["licitacao_utilizador_email"].ToString(),
                                    idLeilao = Convert.ToInt32(leilaoReader["leilao_idLeilao"]),
                                    data = Convert.ToDateTime(leilaoReader["licitacao_data"]),
                                    valor = Convert.ToDouble(leilaoReader["licitacao_valor"])
                                };

                                leilao.licitacoes.Add(licitacao);
                            }

                            activeLeiloes.Add(leilao);
						}
					}
				}

				con.Close();
			}

			return activeLeiloes;
		}




	}

}


