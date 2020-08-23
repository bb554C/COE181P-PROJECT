using GradeViewer.Forms;
using System.Windows;

namespace GradeViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void buttonBackOffice_Click(object sender, RoutedEventArgs e)
        {
            PinCode Pin = new PinCode();
            Pin.MW = this;
            this.Opacity = 0.4;
            Pin.ShowDialog();
        }
    }
}
