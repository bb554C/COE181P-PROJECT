using GradeViewer.Forms;
using StoreManagementSystem.Source_Code.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace GradeViewer.Classes
{
    class UserAccDB
    {
        public static int UserIDPublic { get; set; }
        public static string firstNamePublic { get; set; }
        public static string lastNamePublic { get; set; }
        public static string pinCodePublic { get; set; }

        //Linked List
        public class Node
        {
            public Node next = null;
            public Node last = null;
            public int UserIDNode;
            public string firstNameNode;
            public string lastNameNode;
        }
        public class LinkedList
        {
            private Node head = null;
            private Node tracker = null;
            private Node printTracker = null;
            public void AddLast(int UserID,string firstName, string LastName)
            {
                Node newNode = new Node();
                newNode.UserIDNode = UserID;
                newNode.firstNameNode = firstName;
                newNode.lastNameNode = LastName;
                if(head == null)
                {
                    head = newNode;
                    tracker = newNode;
                    printTracker = newNode;
                }
                else
                {
                    tracker.next = newNode;
                    newNode.last = tracker;
                    tracker = newNode;
                }
            }
            public bool GetNext()
            {
                bool eof = false;
                Node current = printTracker;
                if(printTracker != null)
                {
                    UserIDPublic = current.UserIDNode;
                    firstNamePublic = current.firstNameNode;
                    lastNamePublic = current.lastNameNode;
                }
                if (printTracker == null)
                {
                    eof = true;
                    printTracker = head;
                }
                else
                    printTracker = current.next;
                return eof;
            }
        }
        //End of Linked List
        public static string CreateInitialTable()
        {
            string SQLCreateTable = "CREATE TABLE User_Account_Table ("
            + "UserID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "First_Name TEXT,"
            + "Last_Name TEXT,"
            + "Pincode TEXT NOT NULL UNIQUE);";
            return SQLCreateTable;
        }
        public static string CreateInitialUser()
        {
            string SQLInitialData = "INSERT INTO User_Account_Table"
            + " ( First_Name, Last_Name, Pincode)"
            + " VALUES ( 'ADMIN', 'USER', '" + Encryption.ComputeSha256Hash("00000") + "' );";
            return SQLInitialData;
        }
        public static void AddNewUser(string FirstName,string LastName, string PinCode)
        {
            string SQLInsertData = "INSERT INTO User_Account_Table"
            + " ( First_Name, Last_Name, Pincode)"
            + " VALUES ( '"+ FirstName + "', '"+ LastName +"', '" + Encryption.ComputeSha256Hash(PinCode) + "' );";
            Database.ExecuteSQL(SQLInsertData);
        }
        public static bool DuplicatePinCode(string PinCode)
        {
            if (GetExistingPinCodeFromDB(PinCode) == Encryption.ComputeSha256Hash(PinCode))
            {
                return true;
            }
            return false;
        }
        public static string GetExistingPinCodeFromDB(string PinCode)
        {
            string pinCodeDB = "";
            string SQLCommandString = "SELECT Pincode FROM User_Account_Table WHERE Pincode='" + Encryption.ComputeSha256Hash(PinCode) + "'";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            SQLReader.Read();
            if (SQLReader.HasRows)
            {
                    pinCodeDB = SQLReader.GetString(0);
            }
            SQLConnection.Close();
            return pinCodeDB;
        }
        public static string GetValidUserID(string PinCode)
        {
            string retUserID = "null";
            string SQLCommandString = "SELECT * FROM User_Account_Table WHERE Pincode='" + Encryption.ComputeSha256Hash(PinCode) + "'";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            SQLReader.Read();
            if (SQLReader.HasRows)
            {
                retUserID = (SQLReader.GetInt32(0)).ToString();
                firstNamePublic = SQLReader.GetString(1);
                lastNamePublic = SQLReader.GetString(2);
            }
            SQLConnection.Close();
            return retUserID.ToString();
        }
        public static void CreateUserLoginTMP(string PinCode)
        {
            string pathString = Database.DBFolderPath();
            pathString = Path.Combine(pathString, "UserLogin.tmp");
            StreamWriter writer = new StreamWriter(pathString);
            writer.WriteLine(GetValidUserID(PinCode));
            writer.WriteLine(firstNamePublic);
            writer.WriteLine(lastNamePublic);
            writer.Close();
        }
        public static void DeleteUserAccountTMP()
        {
            string pathString = Database.DBFolderPath();
            pathString = Path.Combine(pathString, "UserLogin.tmp");
            File.Delete(pathString);
        }

        public static void GetFullUserList(ref LinkedList LL)
        {
            string SQLCommandString = "SELECT * FROM User_Account_Table";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            if(SQLReader.HasRows)
            {
                while (SQLReader.Read())
                {
                    LL.AddLast(SQLReader.GetInt32(0), SQLReader.GetString(1), SQLReader.GetString(2));
                }
            }
            SQLConnection.Close();
        }
        public static bool ValidNextNode(ref LinkedList LL)
        {
            return LL.GetNext();
        }
    }
}
