using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using leiloes_monet.Pages;
using System.Reflection.PortableExecutable;


namespace leiloes_monet.DAL
{
    public class UserDAL
    {
        private string connectionString = DALconfig.connectionstring;

        public UserDAL()
        {
            connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }


        public List<Utilizador> GetAllUsers()
        {
            List<Utilizador> users = new List<Utilizador>();

            //string query = @"SELECT * FROM [Li4].[Utilizador];";
            string query = @"SELECT * FROM Li4.Utilizador as u JOIN Li4.Morada as m ON m.idMorada = u.Morada_idMorada;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Morada address = new Morada(rdr.GetInt32(6), rdr.GetString(7), rdr.GetString(8), rdr.GetString(9), rdr.GetString(10));
                    Utilizador user = new Utilizador(rdr.GetString(0), rdr.GetString(1), rdr.GetDateTime(2), rdr.GetString(3), rdr.GetString(4), address);
                    users.Add(user);
                }
            }

            return users;
        }

        public void addUser(string email, string username, DateTime? data, string nif, string password, string rua, string cidade, string cod_postal, string pais)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query1 = @"INSERT INTO Li4.Morada (rua, cidade, cod_postal, pais)
                        VALUES
                        (@Rua, @Cidade, @CodPostal, @Pais)";
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("@Rua", rua);
                    cmd.Parameters.AddWithValue("@Cidade", cidade);
                    cmd.Parameters.AddWithValue("@CodPostal", cod_postal);
                    cmd.Parameters.AddWithValue("@Pais", pais);

                    cmd.ExecuteNonQuery();
                }

                // Step 2: Get the last inserted Morada ID
                string query2 = @"SELECT TOP 1 m.idMorada FROM Li4.Morada AS m ORDER BY m.idMorada DESC;";
                int idmorada = 0;
                using (SqlCommand cmd3 = new SqlCommand(query2, con))
                {
                    using (SqlDataReader rdr = cmd3.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            idmorada = rdr.GetInt32(0);
                        }
                    }
                }

                // Step 3: Insert into Li4.Utilizador
                string query3 = @"INSERT INTO Li4.Utilizador (email, nome, data_nascimento, NIF, password, Morada_idMorada)
                        VALUES
                        (@Email, @Nome, @DataNascimento, @NIF, @Password, @MoradaID)";
                using (SqlCommand cmd2 = new SqlCommand(query3, con))
                {
                    cmd2.Parameters.AddWithValue("@Email", email);
                    cmd2.Parameters.AddWithValue("@Nome", username);
                    cmd2.Parameters.AddWithValue("@DataNascimento", data);
                    cmd2.Parameters.AddWithValue("@NIF", nif);
                    cmd2.Parameters.AddWithValue("@Password", password);
                    cmd2.Parameters.AddWithValue("@MoradaID", idmorada);

                    cmd2.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public bool isEmailTaken(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT COUNT(*) AS EmailCount
                                FROM Li4.Utilizador
                                WHERE email = @Email;";
                int count = 0;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            count = rdr.GetInt32(0);
                        }
                    }
                }
                return count == 1;
            }
        }

        public bool isNIFTaken(string nif)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT COUNT(*) AS NifCount
                                FROM Li4.Utilizador
                                WHERE NIF = @NIF;";
                int count = 0;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NIF", nif);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            count = rdr.GetInt32(0);
                        }
                    }
                }
                return count == 1;
            }
        }

       

    }
    
}