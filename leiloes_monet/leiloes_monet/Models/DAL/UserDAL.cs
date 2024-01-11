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
                                (@Rua, @Cidade, @Pais, @CodPostal);
                                SELECT SCOPE_IDENTITY() As idMorada;"; // Retrieve the generated identity value

                int idMorada;

                using (SqlCommand moradaCmd = new SqlCommand(moradaQuery, con))
                {
                    // Set Morada parameters
                    moradaCmd.Parameters.AddWithValue("@Rua", user.morada.rua);
                    moradaCmd.Parameters.AddWithValue("@Cidade", user.morada.cidade);
                    moradaCmd.Parameters.AddWithValue("@Pais", user.morada.pais);
                    moradaCmd.Parameters.AddWithValue("@CodPostal", user.morada.cod_postal);

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


    }
}
