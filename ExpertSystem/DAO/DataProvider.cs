using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem.DAO
{
    public class DataProvider
    {
        string str_conn = "Data Source=DESKTOP-85J4UFB;Initial Catalog=ExpertSystem;User ID=sa;Password=BitterVirgin99";
        public DataTable ExcuteQuery(string query)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(str_conn))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
        public void Run(string sql)
        {
            SqlConnection connection = new SqlConnection(str_conn);
            connection.Open();
            SqlCommand cmd;
            cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }

        public void Insert(string sql)
        {
            SqlConnection conn = new SqlConnection(str_conn);

            SqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}
