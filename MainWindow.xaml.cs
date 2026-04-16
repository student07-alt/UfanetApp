using System.Windows;

namespace UfanetApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenTable_Click(object sender, RoutedEventArgs e)
        {
            var tableWindow = new TableWindow();
            tableWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}