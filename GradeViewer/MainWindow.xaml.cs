using GradeViewer.Classes;
using GradeViewer.Forms;
using System.Windows;

namespace GradeViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.CreateInitialDatabase();
        }
        private void buttonBackOffice_Click(object sender, RoutedEventArgs e)
        {
            PinCode Pin = new PinCode();
            BackOffice BO = new BackOffice();
            BO.mainWindow = this;
            Pin.MW = this;
            this.Opacity = 0.4;
            Pin.ShowDialog();
            if(Pin.IsValidUser)
            {
                this.Hide();
                BO.ShowDialog();
            }
        }
        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserAccDB.DeleteUserAccountTMP();
            Application.Current.Shutdown();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            DisplayGrades DG = new DisplayGrades();
            this.Hide();
            DG.ShowDialog();
            this.Show();
        }
    }
}
