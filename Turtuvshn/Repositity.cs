using System;
using MySql.Data.MySqlClient;

namespace Turtuvshn
{
    internal class Repositity
    {
        public class MovieRepository
        {
            public string GetNextMovieID()
            {
                string newID = "M100"; 

                try
                {
                    using (var conn = DataHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = "SELECT movieid FROM movie ORDER BY LENGTH(movieid) DESC, movieid DESC LIMIT 1";

                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            object result = cmd.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                string lastIDStr = result.ToString();
                                if (lastIDStr.StartsWith("M") && lastIDStr.Length > 1)
                                {
                                    int currentNumber = int.Parse(lastIDStr.Substring(1));
                                    newID = "M" + (currentNumber + 1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ID үүсгэхэд алдаа гарлаа: " + ex.Message);
                }

                return newID;
            }
        }
    }
}

