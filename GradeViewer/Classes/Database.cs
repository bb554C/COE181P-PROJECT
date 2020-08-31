using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace GradeViewer.Classes
{
    class Database
    {

        public static string DBPath()
        {
            string pathString = Directory.GetCurrentDirectory();
            pathString = DBFolderPath();
            pathString = Path.Combine(pathString, "GradeViewerDB.db");
            return pathString;
        }
        public static string DBFolderPath()
        {
            string pathString = Directory.GetCurrentDirectory();
            Directory.CreateDirectory("Database");
            pathString = Path.Combine(pathString, "Database");
            return pathString;
        }
        public static bool CheckDatabaseExist()
        {
            bool databaseExist = false;
            if (File.Exists(DBPath()))
            {
                databaseExist = true;
            }
            return databaseExist;
        }
        public static void CreateInitialDatabase()
        {
            if (!File.Exists(DBPath()))
            {
                SQLiteConnection.CreateFile(@"Database\GradeViewerDB.db");
                ExecuteSQL(UserAccDB.CreateInitialTable(), UserAccDB.CreateInitialUser());
                ExecuteSQL(GradesDB.CreateInitialTable());
            }
        }
        public static void ExecuteSQL(string SQLCommandString)
        {
            SQLiteConnection SQLConnection = CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLCMD.ExecuteNonQuery();
            SQLConnection.Close();
        }
        public static void ExecuteSQL(string SQLCreateTable, string SQLInitialTable)
        {
            SQLiteConnection SQLConnection = CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCreateTable;
            SQLCMD.ExecuteNonQuery();
            SQLCMD.CommandText = SQLInitialTable;
            SQLCMD.ExecuteNonQuery();
            SQLConnection.Close();
        }
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection SQLConnection;
            SQLConnection = new SQLiteConnection(@"Data Source=Database\GradeViewerDB.db;Version=3;New=True;Compress=True;");
            try
            {
                SQLConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error", "Error Code: " + ex, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return SQLConnection;
        }
    }
}
