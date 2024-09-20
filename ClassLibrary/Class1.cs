using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;

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
                var interopLibraryPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "SQLite.Interop.dll" 
                    : Path.Combine(AppContext.BaseDirectory, "runtimes", "linux-x64", "native", "SQLite.Interop.dll");
                connection.LoadExtension(interopLibraryPath, "sqlite3_fts5_init");
                connection.Close();
            }
        }

        public static long? TestDatabaseEdit()
        {
            var databaseExists = File.Exists("test.db");
            using (var connection = new SQLiteConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                if (!databaseExists)
                {
                    cmd.CommandText = "CREATE TABLE [Test] ([Int] integer NOT NULL);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Test VALUES (@Int)";
                    cmd.Parameters.AddWithValue("Int", 333);
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "SELECT Int FROM Test LIMIT 1";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return reader.IsDBNull(0) ? null : (long?)reader.GetInt64(0);
                    else
                        return null;
                }
            }
        }
    }
}