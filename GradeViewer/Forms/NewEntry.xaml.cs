using GradeViewer.Classes;
using System;
using System.Windows;
using System.Windows.Media;

namespace GradeViewer.Forms
{
    /// <summary>
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        public BackOffice backOffice { get; set; }
        public NewEntry()
        {
            InitializeComponent();
            PopulateComboBox();
        }
        private void PopulateComboBox()
        {
            comboBoxAY.Items.Add("");
            int year = DateTime.Now.Year;
            for (int i = 0; i <= 10; i++)
            {
                comboBoxAY.Items.Add("AY " + year.ToString() + "-" + (year + 1).ToString());
                year = year - 1;
            }
            comboBoxQuarter.Items.Add("");
            for (int i = 1; i <= 4; i++)
            {
                comboBoxQuarter.Items.Add(i.ToString() + "Q");
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NewEntryClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backOffice.Opacity = 1;
        }
        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxAY.SelectedIndex != 0 && 
                comboBoxQuarter.SelectedIndex != 0 && 
                textBoxCourseCode.Text != "" && 
                textBoxStudentNumber.Text != "" && 
                Convert.ToInt32(textBoxStudentNumber.Text) > 0 && 
                textBoxStudentFirstName.Text != "" &&
                textBoxStudentLastName.Text != "" && 
                textBoxGrade.Text != "" && 
                Convert.ToDecimal(textBoxGrade.Text) >= 0 && 
                Convert.ToDecimal(textBoxGrade.Text) <= 100)
            {
                GradesDB.AddNewItem(comboBoxAY.SelectedValue.ToString(), comboBoxQuarter.SelectedValue.ToString(), textBoxCourseCode.Text, Convert.ToInt32(textBoxStudentNumber.Text), textBoxStudentFirstName.Text, textBoxStudentLastName.Text, decimal.Round(Convert.ToDecimal(textBoxGrade.Text), 2, MidpointRounding.AwayFromZero));
                this.Close();
            }
        }
        private void ComboBoxAY_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(comboBoxAY.SelectedIndex == 0)
            {
                comboBoxAYBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                comboBoxAYBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void ComboBoxQuarter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxQuarter.SelectedIndex == 0)
            {
                comboBoxQuarterBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                comboBoxQuarterBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void textBoxCourseCode_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (textBoxCourseCode.Text == "")
            {
                textBoxCourseCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                textBoxCourseCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void textBoxStudentNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (textBoxStudentNumber.Text == "" || !(Convert.ToInt32(textBoxStudentNumber.Text) > 0))
                {
                    textBoxStudentNumberBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                }
                else
                    textBoxStudentNumberBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
            }
            catch(Exception x)
            {
                MessageBox.Show("Student Number must be a number greater than 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxStudentNumber.Text = "";
            }
        }
        private void textBoxStudentFirstName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (textBoxStudentFirstName.Text == "")
            {
                textBoxStudentFirstNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                textBoxStudentFirstNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void textBoxStudentLastName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (textBoxStudentLastName.Text == "")
            {
                textBoxStudentLastNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                textBoxStudentLastNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void textBoxGrade_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (textBoxGrade.Text == "" || !(Convert.ToDecimal(textBoxGrade.Text) >= 0 && Convert.ToDecimal(textBoxGrade.Text) <= 100))
                {
                    textBoxGradeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                }
                else
                    textBoxGradeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
            }
            catch (Exception x)
            {
                MessageBox.Show("Grade must be a number between 0.00 to 100.00", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxGrade.Text = "";
            }

        }
    }
}
