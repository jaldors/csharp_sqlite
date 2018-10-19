using System;
using System.Data.SQLite;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            SQLiteConnection sqliteConnection   = null;
            SQLiteCommand sqliteCommand         = null;
            try
            {
                // Create connection
                using (sqliteConnection = new SQLiteConnection("DataSource=:memory:"))
                {
                    sqliteConnection.Open();

                    // Create Table
                    string sqlCreateTable = "create table highScore (name varchar(10), score int);";
                    sqliteCommand = new SQLiteCommand(sqlCreateTable, sqliteConnection);
                    sqliteCommand.ExecuteNonQuery();

                    string sqlInsert = "insert into highScore (name, score) values ('{0}', {1});";
                    // Insert data
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(string.Format(sqlInsert, "Nome" + i.ToString(), i));
                        sqliteCommand = new SQLiteCommand(string.Format(sqlInsert, "Nome" + i.ToString(), i), sqliteConnection);
                        int r = sqliteCommand.ExecuteNonQuery();
                    }

                    string sqlSelect = "select * from highScore;";
                    sqliteCommand = new SQLiteCommand(sqlSelect, sqliteConnection);
                    SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();

                    if (sqliteDataReader.HasRows)
                    {
                        while (sqliteDataReader.Read())
                        {
                            Console.WriteLine(sqliteDataReader[0].ToString() + " " + sqliteDataReader[1].ToString());
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return;
        }
    }
}
