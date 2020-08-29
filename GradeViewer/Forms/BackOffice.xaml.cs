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

namespace GradeViewer.Forms
{
    /// <summary>
    /// Interaction logic for BackOffice.xaml
    /// </summary>
    public partial class BackOffice : Window
    {

        public BackOffice()
        {
            InitializeComponent();
            UserListReload();
        }

        private void buttonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.4;
            NewUser NU = new NewUser();
            NU.backOffice = this;
            NU.ShowDialog();
            UserListReload();
        }

        private void buttonReload_Click(object sender, RoutedEventArgs e)
        {
            UserListReload();
        }

        private void UserListTabFocus(object sender, RoutedEventArgs e)
        {
            UserListReload();
        }
        public class UserListTMP
        {
            public string userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }
        private void UserListReload()
        {
            UserAccDB.LinkedList LL = new UserAccDB.LinkedList();
            UserAccDB.GetFullUserList(ref LL);
            if(dataGridUserList.Columns.Count < 3)
            {
                DataGridTextColumn c1 = new DataGridTextColumn();
                DataGridTextColumn c2 = new DataGridTextColumn();
                DataGridTextColumn c3 = new DataGridTextColumn();
                c1.Header = "User ID";
                c2.Header = "First Name";
                c3.Header = "Last Name";
                c1.Binding = new Binding("userID");
                c2.Binding = new Binding("firstName");
                c3.Binding = new Binding("lastName");
                dataGridUserList.Columns.Add(c1);
                dataGridUserList.Columns.Add(c2);
                dataGridUserList.Columns.Add(c3);
            }
            dataGridUserList.Items.Clear();
            while (!UserAccDB.ValidNextNode(ref LL))
            {
                dataGridUserList.Items.Add(new UserListTMP { userID = UserAccDB.UserIDPublic.ToString(), firstName = UserAccDB.firstNamePublic.ToString(), lastName = UserAccDB.lastNamePublic.ToString() });
            }
        }
    }
}
