using System.Data.SQLite;

namespace ClassLibrary
{
    public class Class1
    {
        private static string connectionString = new SQLiteConnectionStringBuilder { DataSource = "test.db" }.ToString();

        public static void TestConnectionFTS5Init()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                connection.EnableExtensions(true);
                connection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init");
                connection.Close();
            }
        }
    }
}