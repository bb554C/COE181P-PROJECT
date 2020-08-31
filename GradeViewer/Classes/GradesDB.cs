using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem.Source_Code.Classes;

namespace GradeViewer.Classes
{
    class GradesDB
    {
        public static int gradeIDPublic { get; set; }
        public static string academicYearPublic { get; set; }
        public static string quarterPublic { get; set; }
        public static string courseCodePublic { get; set; }
        public static int studentNumberPublic { get; set; }
        public static string studentFirstNamePublic { get; set; }
        public static string studentLastNamePublic { get; set; }
        public static decimal gradePublic { get; set; }
        //Linked List
        public class Node
        {
            public Node next = null;
            public Node last = null;
            public int GradeIDNode;
            public string academicYearNode;
            public string quarterNode;
            public string courseCodeNode;
            public int studentNumberNode;
            public string studentFirstNameNode;
            public string studentLastNameNode;
            public decimal gradeNode;
        }
        public class LinkedList
        {
            private Node head = null;
            private Node tracker = null;
            private Node printTracker = null;
            public void AddLast(int GradeID ,string AcademicYear, string Quarter, string CourseCode, int StudentNumber, string StudentFirstName, string StudentLastName, decimal Grade)
            {
                Node newNode = new Node();
                newNode.GradeIDNode = GradeID;
                newNode.academicYearNode = AcademicYear;
                newNode.quarterNode = Quarter;
                newNode.courseCodeNode = CourseCode;
                newNode.studentNumberNode = StudentNumber;
                newNode.studentFirstNameNode = StudentFirstName;
                newNode.studentLastNameNode = StudentLastName;
                newNode.gradeNode = Grade;
                if (head == null)
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
            public bool GetNextNode()
            {
                bool eof = false;
                Node current = printTracker;
                if (printTracker != null)
                {
                    gradeIDPublic = current.GradeIDNode;
                    academicYearPublic = current.academicYearNode;
                    quarterPublic = current.quarterNode;
                    courseCodePublic = current.courseCodeNode;
                    studentNumberPublic = current.studentNumberNode;
                    studentFirstNamePublic = current.studentFirstNameNode;
                    studentLastNamePublic = current.studentLastNameNode;
                    gradePublic = current.gradeNode;
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
            public void NextNode()
            {
                Node current = printTracker;
                if (printTracker != null)
                {
                    gradeIDPublic = current.GradeIDNode;
                    academicYearPublic = current.academicYearNode;
                    quarterPublic = current.quarterNode;
                    courseCodePublic = current.courseCodeNode;
                    studentNumberPublic = current.studentNumberNode;
                    studentFirstNamePublic = current.studentFirstNameNode;
                    studentLastNamePublic = current.studentLastNameNode;
                    gradePublic = current.gradeNode;
                }
                if (printTracker == null)
                {
                    printTracker = head;
                }
                else
                    printTracker = current.next;
            }
        }
        //End of Linked List
        public static string CreateInitialTable()
        {
            string SQLCreateTable = "CREATE TABLE Grades_Table ("
            + "GradeID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Academic_Year TEXT NOT NULL,"
            + "Quarter TEXT NOT NULL,"
            + "Course_Code TEXT NOT NULL,"
            + "Student_Number INTEGER NOT NULL,"
            + "Student_First_Name TEXT NOT NULL,"
            + "Student_Last_Name TEXT NOT NULL,"
            + "Grade NUMERIC NOT NULL);";
            return SQLCreateTable;
        }
        public static void AddNewItem(string AcademicYear, string Quarter, string CourseCode,int StudentNumber,string StudentFirstName,string StudentLastName,decimal Grade)
        {
            string SQLInsertData = "INSERT INTO Grades_Table"
            + " ( Academic_Year, Quarter, Course_Code, Student_Number, Student_First_Name, Student_Last_Name, Grade)"
            + " VALUES ( '"+ AcademicYear +"' , '" + Quarter + "' , '" + CourseCode + "' , '" + StudentNumber + "' , '" + StudentFirstName + "' , '" + StudentLastName + "' , '" + Grade + "'); ";
            Database.ExecuteSQL(SQLInsertData);
        }
        public static void GetFullGradesList(ref LinkedList LL)
        {
            string SQLCommandString = "SELECT * FROM Grades_Table";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            if (SQLReader.HasRows)
            {
                while (SQLReader.Read())
                {
                    LL.AddLast(SQLReader.GetInt32(0), SQLReader.GetString(1), SQLReader.GetString(2), SQLReader.GetString(3), SQLReader.GetInt32(4), SQLReader.GetString(5), SQLReader.GetString(6), SQLReader.GetDecimal(7));
                }
            }
            SQLConnection.Close();
        }
        public static bool ValidNextNode(ref LinkedList LL)
        {
            return LL.GetNextNode();
        }
        public static void NextNode(ref LinkedList LL)
        {
            LL.NextNode();
        }
        public static void DeleteGrade(int GradeID)
        {
            string SQLCommandString = "DELETE FROM Grades_Table WHERE GradeID ='" + GradeID + "'";
            Database.ExecuteSQL(SQLCommandString);
        }
        public static bool ValidStudentNumber(int StudentNumber)
        {
            bool valid = false;
            string SQLCommandString = "SELECT Student_Number FROM Grades_Table WHERE Student_Number= '"+StudentNumber.ToString() + "'";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            if (SQLReader.HasRows)
            {
                valid = true;
            }
            SQLConnection.Close();
            return valid;
        }
        public static string getGrade(int StudentNumber, string AcademicYear, string Quarter, string CourseCode)
        {
            string grade = "";
            string SQLCommandString = "SELECT Grade FROM Grades_Table WHERE Student_Number = '" + StudentNumber.ToString() + "' AND Academic_Year = '" + AcademicYear + "' AND Quarter = '" + Quarter + "' AND Course_Code = '" + CourseCode + "'";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLiteDataReader SQLReader = SQLCMD.ExecuteReader();
            SQLReader.Read();
            grade = SQLReader.GetDecimal(0).ToString("0.00") + "%";
            SQLConnection.Close();
            return grade;
        }
    }
}
