using InstallerBuilder.Includes;
using InstallerBuilder.Includes.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace InstallerBuilder.Dialogs
{
    /// <summary>
    /// Interaction logic for BuildDialog.xaml
    /// </summary>
    public partial class BuildDialog : Window
    {
        ObservableCollection<string> _log;
        string _outputFilename;
        string _projectFilename;
        string _projectFilepath;
        string _buildPath;
        IbProject _project;
        IbFactory _factory;


        public BuildDialog(string outputFilename, string projectFilename, IbProject project)
        {
            InitializeComponent();

            this._log = new ObservableCollection<string>();
            this.listProgressLog.DataContext = _log;
            this.listProgressLog.ItemsSource = _log;
            this._outputFilename = outputFilename;
            this._projectFilename = projectFilename;
            this._projectFilepath = !string.IsNullOrWhiteSpace(projectFilename) ? new FileInfo(projectFilename).DirectoryName : null;
            this._project = project;
            this._factory = project.PrepareFactory(_projectFilepath, outputFilename);
        }


        public async Task LogEvent(string format, params object[] args)
        {
            if (format == null) return;
            await Dispatcher.BeginInvoke((Action)delegate
            {
                _log.Add(format);
                listProgressLog.SelectedIndex = listProgressLog.Items.Count - 1;
                listProgressLog.ScrollIntoView(listProgressLog.SelectedItem);
            });
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a temporary directory and copies all needed files into it
            // to guarantee that the file structure can be reflected perfectly.
            await Task_RealizeFilesystem();
        }


        #region Build Tasks


        async Task Task_RealizeFilesystem()
        {
            await LogEvent("Planning File Structure...");
            await Task.Delay(500);
            IbFileSystem entries = _project.CompileFileStructure(_projectFilename);
            _buildPath = _project.GetRoot(_projectFilename);

            /* 
            await Task.Delay(500);
            _buildPath = FileHelper.GetTemporaryDirectory().TrimEnd('\\');
            await LogEvent($"Creating Temporary Folder ({_buildPath})");
            Clipboard.SetText(_buildPath);

            await LogEvent("Preparing Files...");
            foreach (IbFileSystemEntry entry in entries)
            {
                string tmpFile = _buildPath + '\\' + entry.SourceRelativePath;
                if (entry.IsFolder)
                {
                    await LogEvent($"Creating directory {entry.SourceRelativePath}");
                    Directory.CreateDirectory(tmpFile);
                }
                else
                {
                    await LogEvent($"Copying file {entry.SourceRelativePath}...");
                    await FileHelper.CopyAsync(entry.SourceAbsolutePath, tmpFile);
                }

                await Task.Delay(50);
            }
            */

            await LogEvent($"Initializing Factory ({_factory.Name})...");

            _factory.LogEvent += _factory_LogEvent;
            _factory.Complete += _factory_Complete;
            _factory.Begin(_outputFilename, _buildPath, entries);
        }


        private void _factory_Complete(object sender, EventArgs e)
        {
            try
            {
                _ = _buildPath;
                //Directory.Delete(_buildPath, true);
            }
            catch { }
            MessageBox.Show("Build Complete!");
        }


        private async void _factory_LogEvent(string message)
        {
            await LogEvent(message);
        }


        #endregion
    }
}