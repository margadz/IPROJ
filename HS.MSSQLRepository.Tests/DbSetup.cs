using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HS.MSSQLRepository.Tests
{
    public class DbSetup
    {
        public const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HomeServer;Integrated Security=True";
        private const string FileName = @"scripts\TestData.sql";

        public static void SetupDb()
        {
            string path = GetFilePath();

            string script = File.ReadAllText(path);

            var commnads = GetCommands(script).ToArray();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                foreach (var command in commnads)
                {
                    using (var comm = new SqlCommand(command, conn))
                    {
                        comm.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string GetFilePath()
        {
            var path = Directory.GetCurrentDirectory();

            for (int i = 0; i < 4; i++)
            {
                path = Directory.GetParent(path).ToString();
            }

            return Path.Combine(path, FileName);
        }

        private static IEnumerable<string> GetCommands(string fileContent)
        {
            return Regex.Split(fileContent, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
    }
}