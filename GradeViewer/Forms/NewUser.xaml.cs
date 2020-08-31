using GradeViewer.Classes;
using StoreManagementSystem.Source_Code.Classes;
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

namespace GradeViewer.Forms
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public BackOffice backOffice { get; set; }
        public NewUser()
        {
            InitializeComponent();
            
        }
        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && (passwordBoxConfirmPinCode.Password == passwordBoxPinCode.Password))
            {
                if (!(UserAccDB.DuplicatePinCode(passwordBoxPinCode.Password)))
                {
                    UserAccDB.AddNewUser(textBoxFirstName.Text, textBoxLastName.Text, passwordBoxPinCode.Password);
                    ClosingSequence();
                }
                else
                MessageBox.Show("The pincode is already used. Please use another unique pincode", "PinCode already used", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show( "Please verify the information marked as red", "Incorrect Information", MessageBoxButton.OK, MessageBoxImage.Error);
                
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosingSequence();
        }
        private void ClosingSequence()
        {
            backOffice.Opacity = 1;
            this.Close();
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backOffice.Opacity = 1;
        }
        private void FirstNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxFirstName.Text == "")
            {
                textBoxFirstNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                textBoxFirstNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void LastNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxLastName.Text == "")
            {
                textBoxLastNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                textBoxLastNameBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
        }
        private void PinCodePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBoxPinCode.Password == "")
            {
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));

            if (passwordBoxConfirmPinCode.Password != passwordBoxPinCode.Password)
            {
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                passwordBoxConfirmPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else if (passwordBoxConfirmPinCode.Password != "" || passwordBoxPinCode.Password != "")
            {
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                passwordBoxConfirmPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
            }
        }

        private void ConfirmPinCodePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBoxConfirmPinCode.Password == "")
            {
                passwordBoxConfirmPinCode.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else
                passwordBoxConfirmPinCode.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));

            if (passwordBoxConfirmPinCode.Password != passwordBoxPinCode.Password)
            {
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
                passwordBoxConfirmPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x19, 0x19));
            }
            else if (passwordBoxConfirmPinCode.Password != "" || passwordBoxPinCode.Password != "")
            {
                passwordBoxPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
                passwordBoxConfirmPinCodeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x19, 0xFA));
            }
        }
    }
}
