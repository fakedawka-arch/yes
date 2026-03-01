using MySql.Data.MySqlClient;

namespace Turtuvshn
{
   public static class DataHelperBase
    {
        private static string connStr = "server=localhost;database=gf;port=3306;uid=root;pwd=;";
        public static MySqlConnection GetConnection() // Дата бааз руу нэвтрэх "шугам"-ыг хаанаас ч дуудаж болохоор нээлттэй, бэлэн байдалд бэлдэж байна.
        {
            return new MySqlConnection(connStr); // Хаяг, нууц үгийг нь агуулсан шинэ холболтын объектыг үүсгээд буцааж байна.
        }
    }
}
    
