using System;
using System.IO;
using System.Threading.Tasks;

namespace InstallerBuilder.Includes
{
    public abstract class IbFactory
    {
        public string Name { get; }
        public DirectoryInfo SourceDirectory { get; private set; }
        public IbProject Project { get; private set; }


        public event IbFactoryLogEventHandler LogEvent;
        public event EventHandler Complete;


        protected IbFactory(string name, DirectoryInfo sourceDirectory, IbProject project)
        {
            this.Name = name;
            this.SourceDirectory = sourceDirectory;
            this.Project = project;
        }


        public abstract void Validate();


        public abstract void Begin(string outputFilename, string buildFolder, IbFileSystem fileSystem);
    

        protected void DoLogEvent(string message) => LogEvent?.Invoke(message);


        protected void DoComplete() => Complete?.Invoke(this, new EventArgs());
    }


    public class InstallerFactoryException : Exception
    {
        public InstallerFactoryException(string message) : base(message) { }


        public InstallerFactoryException(string message, Exception innerException) : base(message, innerException) { }
    }


    public delegate void IbFactoryLogEventHandler(string message);
}