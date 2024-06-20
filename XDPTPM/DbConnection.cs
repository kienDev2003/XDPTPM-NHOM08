using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace XDPTPM
{
    public class DbConnection
    {
        public SqlConnection conn = null;
        private string strConn = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;

        public SqlConnection getConnection()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public SqlConnection closeConnection()
        {
            if(conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }

        public int Command(string query)
        {
            using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public SqlDataReader DataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }
    }
}