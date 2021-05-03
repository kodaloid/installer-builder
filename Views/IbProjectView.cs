using InstallerBuilder.Includes;
using System.ComponentModel;

namespace InstallerBuilder.Views
{
    public class IbProjectView :INotifyPropertyChanged
    {
        IbProject _CurrentProject;


        public IbProject Project
        {
            get => _CurrentProject;
            set
            {
                _CurrentProject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Project)));
            }
        }


        public string LastFilename { get; set; }
            
            
        public event PropertyChangedEventHandler PropertyChanged;
    }
}