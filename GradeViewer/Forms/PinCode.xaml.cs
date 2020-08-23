using System.Windows;
using System.Windows.Controls;

namespace GradeViewer.Forms
{
    public partial class PinCode : Window
    {
        private string pin;
        private int pinCounter = 0;
        public bool IsValidUser { get; set; }
        public MainWindow MW { get; set; }
        public PinCode()
        {
            InitializeComponent();
            passwordTextBox.Opacity = 0;
            ClearPinNumber();
        }
        private void ClearPinNumber()
        {
            pinCounter = 0;
            passwordBox.Clear();
            passwordTextBox.Clear();
            pin = "";
            passwordTextBox.Focus();
        }
        private void DisplayPinNumber()
        {
            passwordBox.Password = passwordTextBox.Text;
            pin = passwordTextBox.Text;
            if (pin != "")
            {
                pinCounter++;
            }
            passwordTextBox.Focus();
            CheckPinLength();
        }
        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            MW.Opacity = 1;
            this.Close();
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearPinNumber();
            passwordTextBox.Focus();
        }
        private void ButtonNumber9_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "9";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber8_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "8";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber7_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "7";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber6_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "6";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber5_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "5";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber4_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "4";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber3_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "3";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber2_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "2";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber1_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "1";
            passwordTextBox.Text = pin;
        }
        private void ButtonNumber0_Click(object sender, RoutedEventArgs e)
        {
            pin = pin + "0";
            passwordTextBox.Text = pin;
        }
        private void PinNumberChanged(object sender, TextChangedEventArgs e)
        {
            DisplayPinNumber();
        }
        private void CheckPinLength()
        {
            if (pinCounter > 10)
            {
                MessageBox.Show("Pin code entered is too long. Please enter a pin code less than or equal to 10 digits only.", "Pin Number Length Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Hide();
                this.Show();
                MW.Opacity = 0.4;
                ClearPinNumber();
            }
        }
        private void Close_PinCodeWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MW.Opacity = 1;
        }
    }
}
