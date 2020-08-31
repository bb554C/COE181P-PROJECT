using GradeViewer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace GradeViewer.Forms
{
    /// <summary>
    /// Interaction logic for DisplayGrades.xaml
    /// </summary>
    public partial class DisplayGrades : Window
    {
        public DisplayGrades()
        {
            InitializeComponent();
            labelGrades.Content = "";
        }
        private void ButtonSeaarch_Click(object sender, RoutedEventArgs e)
        {
            labelGrades.Content = GradesDB.getGrade(Convert.ToInt32(textBoxStudentNumber.Text), comboBoxAY.Items[comboBoxAY.SelectedIndex].ToString(), comboBoxQuarter.Items[comboBoxQuarter.SelectedIndex].ToString(), comboBoxCourseCode.Items[comboBoxCourseCode.SelectedIndex].ToString());
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxStudentNumber.Text = "";
            labelGrades.Content = "";
            comboBoxAY.ItemsSource = null;
            comboBoxCourseCode.ItemsSource = null;
            comboBoxQuarter.ItemsSource = null;
            comboBoxAY.Items.Clear();
            comboBoxCourseCode.Items.Clear();
            comboBoxQuarter.Items.Clear();
            textBoxStudentNumber.IsEnabled = true;
            comboBoxAY.IsEnabled = false;
            comboBoxCourseCode.IsEnabled = false;
            comboBoxQuarter.IsEnabled = false;
            buttonSearch.IsEnabled = false;
            textBoxStudentNumberBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            comboBoxAYBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x64, 0x64));
            comboBoxQuarterBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x64, 0x64));
            comboBoxCourseCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x64, 0x64));
        }
        private void TextBoxStudentNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxStudentNumber.Text != "")
                if (GradesDB.ValidStudentNumber(Convert.ToInt32(textBoxStudentNumber.Text)))
                {
                    comboBoxAY.IsEnabled = true;
                    SQLiteConnection SQLConnection = Database.CreateConnection();
                    SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
                    string SQLCommandString = "SELECT DISTINCT Academic_Year FROM Grades_Table WHERE Student_Number = '" + textBoxStudentNumber.Text + "'";
                    SQLCMD.CommandText = SQLCommandString;
                    SQLCMD.ExecuteNonQuery();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
                    dataAdapter = new SQLiteDataAdapter(SQLCMD);
                    DataTable data = new DataTable();
                    dataAdapter.Fill(data);
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        comboBoxAY.Items.Add(data.Rows[i][0].ToString());
                    }
                    textBoxStudentNumber.IsEnabled = false;
                    textBoxStudentNumberBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                    comboBoxAYBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                    SQLConnection.Close();
                }
        }
        private void ComboBoxAY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxAY.SelectedIndex != -1)
            {
                comboBoxQuarter.IsEnabled = true;
                SQLiteConnection SQLConnection = Database.CreateConnection();
                SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
                string SQLCommandString = "SELECT DISTINCT Quarter FROM Grades_Table WHERE Student_Number = '" + textBoxStudentNumber.Text + "' AND Academic_Year = '" + comboBoxAY.Items[comboBoxAY.SelectedIndex].ToString() + "'";
                SQLCMD.CommandText = SQLCommandString;
                SQLCMD.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
                dataAdapter = new SQLiteDataAdapter(SQLCMD);
                DataTable data = new DataTable();
                dataAdapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    comboBoxQuarter.Items.Add(data.Rows[i][0].ToString());
                }
                comboBoxAY.IsEnabled = false;
                comboBoxAYBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                comboBoxQuarterBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                SQLConnection.Close();
            }
        }
        private void ComboBoxQuarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxQuarter.SelectedIndex != -1)
            {
                comboBoxCourseCode.IsEnabled = true;
                SQLiteConnection SQLConnection = Database.CreateConnection();
                SQLiteCommand SQLCMD = SQLConnection.CreateCommand();
                string SQLCommandString = "SELECT DISTINCT Course_Code FROM Grades_Table WHERE Student_Number = '" + textBoxStudentNumber.Text + "' AND Academic_Year = '" + comboBoxAY.Items[comboBoxAY.SelectedIndex].ToString() + "' AND Quarter = '" + comboBoxQuarter.Items[comboBoxQuarter.SelectedIndex].ToString() + "'";
                SQLCMD.CommandText = SQLCommandString;
                SQLCMD.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SQLCMD);
                dataAdapter = new SQLiteDataAdapter(SQLCMD);
                DataTable data = new DataTable();
                dataAdapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    comboBoxCourseCode.Items.Add(data.Rows[i][0].ToString());
                }
                comboBoxQuarter.IsEnabled = false;
                comboBoxQuarterBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                comboBoxCourseCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                SQLConnection.Close();
            }
        }
        private void ComboBoxCourseCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxCourseCode.SelectedIndex != -1)
            {
                comboBoxCourseCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                buttonSearch.IsEnabled = true;
            }
        }

    }
}
