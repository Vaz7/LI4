using System.Collections.Generic;
using System.Data.SqlClient;

namespace leiloes_monet.Models.DAL
{
    public class UserDAL : IUser
    {
        string connectionstring = DALConfig.connectionstring;

        public void addClient(Utilizador user)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                // Step 1: Insert Morada (address) into the database
                string moradaQuery = @"INSERT INTO [Li4].[Morada]
                                ([rua], [cidade], [cod_postal],[pais])
                                VALUES
                                (@Rua, @Cidade, @CodPostal, @Pais);
                                SELECT SCOPE_IDENTITY() As idMorada;"; // Retrieve the generated identity value

                int idMorada;

                using (SqlCommand moradaCmd = new SqlCommand(moradaQuery, con))
                {
                    // Set Morada parameters
                    moradaCmd.Parameters.AddWithValue("@Rua", user.morada.rua);
                    moradaCmd.Parameters.AddWithValue("@Cidade", user.morada.cidade);
                    moradaCmd.Parameters.AddWithValue("@CodPostal", user.morada.cod_postal);
                    moradaCmd.Parameters.AddWithValue("@Pais", user.morada.pais);
                    

                    // Execute the query and get the generated identity value
                    idMorada = Convert.ToInt32(moradaCmd.ExecuteScalar());
                }

                // Step 2: Insert Utilizador (user) into the database with the retrieved idMorada
                string userQuery = @"INSERT INTO [Li4].[Utilizador]
                             ([email], [nome], [data_nascimento], [NIF], [password], [idMorada])
                             VALUES
                             (@Email, @Nome, @DataNascimento, @NIF, @Password, @IdMorada);";

                using (SqlCommand userCmd = new SqlCommand(userQuery, con))
                {
                    // Set Utilizador parameters
                    userCmd.Parameters.AddWithValue("@Email", user.email);
                    userCmd.Parameters.AddWithValue("@Nome", user.nome);
                    userCmd.Parameters.AddWithValue("@DataNascimento", user.data_nascimento);
                    userCmd.Parameters.AddWithValue("@NIF", user.nif);
                    userCmd.Parameters.AddWithValue("@Password", user.password);
                    userCmd.Parameters.AddWithValue("@IdMorada", idMorada); // Use the retrieved idMorada

                    userCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public bool EmailExists(String email)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM [Li4].[Utilizador] WHERE [email] = @Email";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public bool UserExists(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM [Li4].[Utilizador] WHERE [email] = @Email AND [password] = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

		public Utilizador getUser(string email)
		{
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				con.Open();

				string query = @"SELECT u.email, u.nome, u.data_nascimento, u.NIF, u.password,
                        m.rua, m.cidade, m.cod_postal, m.pais
                        FROM [Li4].[Utilizador] u
                        INNER JOIN [Li4].[Morada] m ON u.idMorada = m.idMorada
                        WHERE u.email = @Email";

				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@Email", email);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							// Create a Utilizador object and populate its properties
							Utilizador user = new Utilizador
							{
								email = reader["email"].ToString(),
								nome = reader["nome"].ToString(),
								data_nascimento = (DateTime)reader["data_nascimento"],
								nif = reader["NIF"].ToString(),
								password = reader["password"].ToString(),
								morada = new Morada
								{
									rua = reader["rua"].ToString(),
									cidade = reader["cidade"].ToString(),
									cod_postal = reader["cod_postal"].ToString(),
									pais = reader["pais"].ToString()
								}
							};
							return user;
						}
					}
				}

				return null; // Return null if the user with the given email is not found
			}
		}

		public bool updateClient(Utilizador user)
		{
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				con.Open();

				// Fetch the idMorada from the Utilizador table
				string getIdMoradaQuery = "SELECT idMorada FROM [Li4].[Utilizador] WHERE email = @Email";

				int idMorada;
				using (SqlCommand getIdMoradaCmd = new SqlCommand(getIdMoradaQuery, con))
				{
					getIdMoradaCmd.Parameters.AddWithValue("@Email", user.email);
					idMorada = Convert.ToInt32(getIdMoradaCmd.ExecuteScalar());
				}

				// Step 1: Update Morada (address) in the database
				string moradaQuery = @"UPDATE [Li4].[Morada]
                               SET [rua] = @Rua, [cidade] = @Cidade, [cod_postal] = @CodPostal, [pais] = @Pais
                               WHERE [idMorada] = @IdMorada;";

				using (SqlCommand moradaCmd = new SqlCommand(moradaQuery, con))
				{
					// Set Morada parameters
					moradaCmd.Parameters.AddWithValue("@IdMorada", idMorada); // Use the fetched idMorada
					moradaCmd.Parameters.AddWithValue("@Rua", user.morada.rua);
					moradaCmd.Parameters.AddWithValue("@Cidade", user.morada.cidade);
					moradaCmd.Parameters.AddWithValue("@CodPostal", user.morada.cod_postal);
					moradaCmd.Parameters.AddWithValue("@Pais", user.morada.pais);

					moradaCmd.ExecuteNonQuery();
				}

				// Step 2: Update Utilizador (user) in the database
				string userQuery = @"UPDATE [Li4].[Utilizador]
                             SET [nome] = @Nome, [data_nascimento] = @DataNascimento, [NIF] = @NIF, [password] = @Password
                             WHERE [email] = @Email;";

				using (SqlCommand userCmd = new SqlCommand(userQuery, con))
				{
					// Set Utilizador parameters
					userCmd.Parameters.AddWithValue("@Email", user.email);
					userCmd.Parameters.AddWithValue("@Nome", user.nome);
					userCmd.Parameters.AddWithValue("@DataNascimento", user.data_nascimento);
					userCmd.Parameters.AddWithValue("@NIF", user.nif);
					userCmd.Parameters.AddWithValue("@Password", user.password);

					userCmd.ExecuteNonQuery();
				}

				con.Close();
				return true; // Return true if the update is successful
			}
		}






	}
}
