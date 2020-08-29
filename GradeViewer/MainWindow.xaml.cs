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
            Pin.MW = this;
            this.Opacity = 0.4;
            Pin.ShowDialog();
            if(Pin.IsValidUser)
            {
                BO.ShowDialog();
            }
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserAccDB.DeleteUserAccountTMP();
            Application.Current.Shutdown();
        }
    }
}
