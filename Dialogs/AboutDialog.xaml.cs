using System;
using System.Diagnostics;
using System.Reflection;
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

            if (Assembly.GetExecutingAssembly().GetName().Version is Version v)
            {

                TextVersion.Text = $"Version {v.Major}.{v.Minor} (build {v.Build.ToString().PadLeft(3, '0')}) - Copyright " + DateTime.Now.Year;

            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://kodaloid.com/projects/installer-builder/") { UseShellExecute = true });
        }
    }
}