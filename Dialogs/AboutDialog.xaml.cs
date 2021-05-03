using System.Diagnostics;
using System.Windows;

namespace InstallerBuilder.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://kodaloid.com/projects/installer-builder/") { UseShellExecute = true });
        }
    }
}