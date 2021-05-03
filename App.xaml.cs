using System.IO;
using System.Windows;

namespace InstallerBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static string FileToLoad { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                FileInfo file = new FileInfo(e.Args[0]);
                if (file.Exists && file.Extension.ToLowerInvariant() == ".ibf")
                {
                    FileToLoad = file.FullName;
                }
                else
                {
                    MessageBox.Show("Installer Builder", "Error! unrecognised file extension!");
                }
            }
        }
    }
}