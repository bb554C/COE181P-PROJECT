using System.Windows;

namespace GradeViewer.Forms
{
    /// <summary>
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        public NewEntry()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newEntryClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }
    }
}
