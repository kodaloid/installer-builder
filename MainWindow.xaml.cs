using InstallerBuilder.Dialogs;
using InstallerBuilder.Includes;
using InstallerBuilder.Includes.Helpers;
using InstallerBuilder.Views;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace InstallerBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IbProjectView theView;
        bool normalLaunch;


        public MainWindow()
        {
            InitializeComponent();
            //tb1.Text = "Hello World";
            if (!string.IsNullOrWhiteSpace(App.FileToLoad))
            {
                this.theView = new IbProjectView();
                this.theView.LastFilename = App.FileToLoad;
                this.SetProject(IbProject.Load(App.FileToLoad));
            }
            else
            {
                normalLaunch = true;
                this.theView = new IbProjectView();
                SetProject(new IbProject());
            }
            
        }


        private void SetProject(IbProject proejct)
        {
            this.theView.Project = proejct;
            this.DataContext = theView;
            
        }


        private void CommandCanAlwaysExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;


        private void CommandMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = e.Command as RoutedUICommand;
            switch (cmd?.Name ?? "")
            {
                case "FileNew":
                    SetProject(new IbProject());

                    break;
                case "FileOpen":
                    var dlgOpenJSON = new OpenFileDialog { Filter = "InstallerBuilder File(s)|*.ibf" };
                    if (dlgOpenJSON.ShowDialog(this) == true)
                    {
                        theView.LastFilename = dlgOpenJSON.FileName;
                        SetProject(IbProject.Load(dlgOpenJSON.FileName));
                    }
                    break;
                case "FileSave":
                    if (string.IsNullOrWhiteSpace(theView.LastFilename)) goto saveas;
                    theView.Project.SaveAs(theView.LastFilename);
                    MessageBox.Show("Saved!");
                    break;

                case "FileSaveAs":
                saveas:;
                    var dlgSaveJSON = new SaveFileDialog { Filter = "InstallerBuilder File(s)|*.ibf" };
                    if(dlgSaveJSON.ShowDialog(this) == true)
                    {
                        theView.LastFilename = dlgSaveJSON.FileName;
                        theView.Project.SaveAs(dlgSaveJSON.FileName);
                        MessageBox.Show("Saved!");
                    }
                    break;

                case "BuildPreview":
                    var dlgPreview = new PreviewBuildDialog(theView.Project, theView.LastFilename);
                    dlgPreview.Owner = this;
                    dlgPreview.ShowDialog();
                    break;

                case "BuildNow":
                    var dlgSaveEXE = new SaveFileDialog
                    {
                        Filter = "Executable File(s)|*.exe",
                        FileName = theView.Project.GenerateExeFilename()
                    };

                    if (string.IsNullOrWhiteSpace(tt.Text))
                    {
                        MessageBox.Show("Please choose a source folder first.");
                        return;
                    }

                    if (dlgSaveEXE.ShowDialog(this) == true)
                    {
                        BuildDialog buildDialog = new BuildDialog(dlgSaveEXE.FileName, theView.LastFilename, theView.Project);
                        buildDialog.Owner = this;
                        buildDialog.ShowDialog();
                    }
                    break;

                case "FileExit":
                    Close();
                    break;

                case "About":
                    new AboutDialog { Owner = this }.ShowDialog();
                    break;
            }
        }


        private void buttonLoadLicence_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "All File(s)|*.*";
            if (dlg.ShowDialog(this) == true && !string.IsNullOrWhiteSpace(dlg.FileName))
            {
                textLicense.Text = File.ReadAllText(dlg.FileName);
            }
        }


        private void buttonBrowseSource_ButtonClick(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            if (dlg.ShowDialog(this) == true)
            {
                if (string.IsNullOrWhiteSpace(theView.LastFilename))
                {
                    theView.Project.SourcePath = dlg.SelectedPath;
                }
                else
                {
                    FileInfo info = new FileInfo(theView.LastFilename);
                    theView.Project.SourcePath = dlg.SelectedPath.Replace(info.DirectoryName, "");
                }
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AssocHelper.IsAssociationsSet() && normalLaunch)
            {
                if (MessageBox.Show("Associate .ibf files with Installer Builder?", "File Association", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AssocHelper.EnsureAssociationsSet();
                }
            }
        }
    }
}