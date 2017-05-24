using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Datamining
{
    static class SQLDB
    {
        const string user = "root";
        const string password = "root";
        const string server = "localhost";
        const string database = "gutenberg";
        static MySqlConnection conn;

        public static MySqlConnection GetConnection()
        {
            if(conn == null)
            {
                conn = new MySqlConnection(string.Format("Server=localhost;Database=gutenberg;Uid=root;Pwd=root;"));
            }
            try
            {
                conn.Open();
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
    }
}
