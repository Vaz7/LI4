using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using leiloes_monet.Pages;


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

            string query = @"SELECT * FROM [Li4].[Utilizador];";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Utilizador user = new Utilizador(rdr.GetString(0), rdr.GetString(1), rdr.GetDateTime(2), rdr.GetString(3), rdr.GetString(4));
                    users.Add(user);
                }
            }

            return users;
        }
    }
}