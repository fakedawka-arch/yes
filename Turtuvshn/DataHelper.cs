using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtuvshn
{
    public static class DataHelper
    {
        
        private static string connStr = "server=localhost;database=gf;port=3306;uid=root;pwd=;";
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }
    }
}
    

