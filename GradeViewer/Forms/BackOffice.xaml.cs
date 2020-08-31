using GradeViewer.Classes;
using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GradeViewer.Forms
{
    public partial class BackOffice : Window
    {
        public MainWindow mainWindow { get; set; }
        public BackOffice()
        {
            InitializeComponent();
        }
        private void buttonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.4;
            NewUser NU = new NewUser();
            NU.backOffice = this;
            NU.ShowDialog();
            UserListReload();
        }
        private void buttonAddItem_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.4;
            NewEntry NE = new NewEntry();
            NE.backOffice = this;
            NE.ShowDialog();
            GradesListReload();
        }
        private void buttonGradesListReload_Click(object sender, RoutedEventArgs e)
        {
            GradesListReload();
        }
        private void buttonReloadUserList_Click(object sender, RoutedEventArgs e)
        {
            UserListReload();
        }
        private void UserListReload()
        {
            dataGridUserList.ItemsSource = null;
            dataGridUserList.Items.Clear();
            string SQLCommandString = "SELECT UserID, First_Name, Last_Name FROM User_Account_Table";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLCMD.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
            DataTable dataTable = new DataTable("User_Account_Table");
            dataAdapter.Fill(dataTable);
            dataGridUserList.ItemsSource = dataTable.DefaultView;
            dataAdapter.Update(dataTable);
            dataGridUserList.Columns[0].Header = "User ID";
            dataGridUserList.Columns[1].Header = "First Name";
            dataGridUserList.Columns[2].Header = "Last Name";
            SQLConnection.Close();
        }
        private void GradesListReload()
        {
            comboBoxAY.ItemsSource = null;
            comboBoxQuarter.ItemsSource = null;
            dataGridGradesList.ItemsSource = null;
            comboBoxAY.Items.Clear();
            comboBoxQuarter.Items.Clear();
            try
            {
                dataGridGradesList.Items.Clear();
            }
            catch (Exception x)
            {

            }
            string SQLCommandString = "SELECT GradeID, Academic_Year, Quarter, Course_Code, Student_Number, Student_First_Name, Student_Last_Name, Grade FROM Grades_Table";
            SQLiteConnection SQLConnection = Database.CreateConnection();
            SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
            SQLCMD.CommandText = SQLCommandString;
            SQLCMD.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
            DataTable dataTable = new DataTable("Grades_Table");
            dataAdapter.Fill(dataTable);
            dataGridGradesList.ItemsSource = dataTable.DefaultView;
            dataAdapter.Update(dataTable);
            dataGridGradesList.Columns[0].Header = "Grade ID";
            dataGridGradesList.Columns[1].Header = "Academic Year";
            dataGridGradesList.Columns[2].Header = "Quarter";
            dataGridGradesList.Columns[3].Header = "Course Code";
            dataGridGradesList.Columns[4].Header = "Student Number";
            dataGridGradesList.Columns[5].Header = "First Name";
            dataGridGradesList.Columns[6].Header = "Last Name";
            dataGridGradesList.Columns[7].Header = "Grade";

            SQLCommandString = "SELECT DISTINCT Academic_Year FROM Grades_Table";
            SQLCMD.CommandText = SQLCommandString;
            SQLCMD.ExecuteNonQuery();
            dataAdapter = new SQLiteDataAdapter(SQLCMD);
            DataTable dataAY = new DataTable();
            dataAdapter.Fill(dataAY);
            for (int i = 0; i < dataAY.Rows.Count; i++)
            {
                comboBoxAY.Items.Add(dataAY.Rows[i]["Academic_Year"].ToString());
            }

            SQLCommandString = "SELECT DISTINCT Quarter FROM Grades_Table";
            SQLCMD.CommandText = SQLCommandString;
            SQLCMD.ExecuteNonQuery();
            dataAdapter = new SQLiteDataAdapter(SQLCMD);
            DataTable dataQ = new DataTable();
            dataAdapter.Fill(dataQ);
            for (int i = 0; i < dataQ.Rows.Count; i++)
            {
                comboBoxQuarter.Items.Add(dataQ.Rows[i]["Quarter"].ToString());
            }
        }
        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGridUserList.SelectedItem;
            UserAccDB.DeleteUser(Convert.ToInt32(dataRow.Row.ItemArray[0]));
            UserListReload();
        }
        private void buttonDeleteGrades_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGridUserList.SelectedItem;
            GradesDB.DeleteGrade(Convert.ToInt32(dataRow.Row.ItemArray[0]));
            UserListReload();
            GradesListReload();
        }
        private void dataUserListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDelete.IsEnabled = true;
        }

        private void backOffice_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }

        private void dataGridGradesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDeleteGrades.IsEnabled = true;
        }

        private void comboBoxAY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDataTable();
        }

        private void comboBoxQuarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDataTable();
        }

        private void comboBoxCourseCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDataTable();
        }

        private void FilterDataTable()
        {
            string SQLCommandString = "";
            if (comboBoxAY.SelectedIndex != -1 && comboBoxQuarter.SelectedIndex != -1)
            {
                SQLCommandString = "SELECT GradeID, Academic_Year, Quarter, Course_Code, Student_Number, Student_First_Name, Student_Last_Name, Grade FROM Grades_Table WHERE Academic_Year = '" + comboBoxAY.Items[comboBoxAY.SelectedIndex].ToString() + "' AND Quarter = '" + comboBoxQuarter.Items[comboBoxQuarter.SelectedIndex].ToString() + "'";
            }
            else if (comboBoxQuarter.SelectedIndex != -1)
            {
                SQLCommandString = "SELECT GradeID, Academic_Year, Quarter, Course_Code, Student_Number, Student_First_Name, Student_Last_Name, Grade FROM Grades_Table WHERE Quarter = '" + comboBoxQuarter.Items[comboBoxQuarter.SelectedIndex].ToString() + "'";
            }
            else if (comboBoxAY.SelectedIndex != -1)
            {
                SQLCommandString = "SELECT GradeID, Academic_Year, Quarter, Course_Code, Student_Number, Student_First_Name, Student_Last_Name, Grade FROM Grades_Table WHERE Academic_Year = '" + comboBoxAY.Items[comboBoxAY.SelectedIndex].ToString() + "'";
            }
            if(SQLCommandString != "")
            {
                dataGridGradesList.ItemsSource = null;
                dataGridGradesList.Items.Clear();
                SQLiteConnection SQLConnection = Database.CreateConnection();
                SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
                SQLCMD.CommandText = SQLCommandString;
                SQLCMD.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
                DataTable dataTable = new DataTable("Grades_Table");
                dataAdapter.Fill(dataTable);
                dataGridGradesList.ItemsSource = dataTable.DefaultView;
                dataAdapter.Update(dataTable);
                dataGridGradesList.Columns[0].Header = "Grade ID";
                dataGridGradesList.Columns[1].Header = "Academic Year";
                dataGridGradesList.Columns[2].Header = "Quarter";
                dataGridGradesList.Columns[3].Header = "Course Code";
                dataGridGradesList.Columns[4].Header = "Student Number";
                dataGridGradesList.Columns[5].Header = "First Name";
                dataGridGradesList.Columns[6].Header = "Last Name";
                dataGridGradesList.Columns[7].Header = "Grade";
            }
        }
    }
}
