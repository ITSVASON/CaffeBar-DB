using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace CaffeBar.DAL
{
    public class PuntoretDAL
    {
        private string connectionString;

        public PuntoretDAL(string connStr)
        {
            connectionString = connStr;
        }
        //READ all specific tableName
        public DataTable GetAllFromTable(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [{tableName}]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }


        // READ all puntoret
        public List<Puntoret> GetAll()
        {
            var list = new List<Puntoret>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM puntoret";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Puntoret()
                    {
                        Id = reader["id"].ToString(),
                        Emri = reader["emri"].ToString(),
                        ContactInfo = reader["contact_info"].ToString(),
                        DataPunsimit = reader["data_punsimit"].ToString()
                    });
                }
            }

            return list;
        }

        public void Insert(Puntoret p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

         
                string checkQuery = "SELECT COUNT(*) FROM puntoret WHERE id = @id";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", p.Id);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        throw new Exception($"Puntori me ID = {p.Id} ekziston tashmë.");
                    }
                }

           
                string insertQuery = "INSERT INTO puntoret (id, emri, contact_info, data_punsimit) " +
                                     "VALUES (@id, @emri, @contact_info, @data_punsimit)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@id", p.Id);
                    insertCmd.Parameters.AddWithValue("@emri", p.Emri);
                    insertCmd.Parameters.AddWithValue("@contact_info", p.ContactInfo);
                    insertCmd.Parameters.AddWithValue("@data_punsimit", p.DataPunsimit);

                    insertCmd.ExecuteNonQuery();
                }
            }
        }


        // UPDATE
        public void Update(Puntoret p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE puntoret SET emri=@emri, contact_info=@contact_info, data_punsimit=@data_punsimit WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@emri", p.Emri);
                cmd.Parameters.AddWithValue("@contact_info", p.ContactInfo);
                cmd.Parameters.AddWithValue("@data_punsimit", p.DataPunsimit);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void DeleteById(string tableName, string idColumnName, string idValue)
        {
          
            var allowedTables = new Dictionary<string, string[]>
    {
        { "puntoret", new[] { "id" } },
        { "p_oraret", new[] { "id" } },
        { "produkte", new[] { "id" } },
        { "shitjet", new[] { "id" } },
        { "shitje_artikuj", new[] { "id" } },
        { "ardhurat_ditore", new[] { "id" } },
        { "hargjimet", new[] { "id" } }
    };

        
            if (!allowedTables.ContainsKey(tableName) || !allowedTables[tableName].Contains(idColumnName))
                throw new ArgumentException("Invalid table or column name.");

            string query = $"DELETE FROM {tableName} WHERE {idColumnName} = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", idValue);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}