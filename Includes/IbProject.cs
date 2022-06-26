using InstallerBuilder.Includes.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace InstallerBuilder.Includes
{
    public sealed class IbProject : INotifyPropertyChanged
    {
        #region Private Members

        string _ProductName, _Description, _Publisher, _Copyright, _Website, _Version,
               _OutputFilename, _StartMenuFolderName, _License, _SourcePath, _SourceIgnores,
               _DesktopShortcutIncludes, _PrimaryExeFilename, _ExtraNsisCode, _ExtraUninstNsisCode;
        SoftwareType _SoftwareType;
        InstallerType _Installer;
        SupportType _SupportFramework;
        RequestExecutionLevelType _RequestExecutionLevel;
        bool _CreateStartMenuShortcut, _IncludeLicensePage, _IncludeUninstaller;

        #endregion


        #region Properties


        /// <summary>
        /// The product name.
        /// </summary>
        public string ProductName
        {
            get => _ProductName;
            set { _ProductName = value; change(nameof(ProductName)); }
        }


        /// <summary>
        /// The product name.
        /// </summary>
        public string ProductNameNoSpaces
        {
            get => _ProductName.Replace(" ", "");
        }


        /// <summary>
        /// The type of software being installed.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SoftwareType SoftwareType
        {
            get => _SoftwareType;
            set { _SoftwareType = value; change(nameof(SoftwareType)); }
        }

        /// <summary>
        /// The description of this software package.
        /// </summary>
        public string Description
        {
            get => _Description;
            set { _Description = value; change(nameof(Description)); }
        }

        /// <summary>
        /// The publisher/author of this software package.
        /// </summary>
        public string Publisher
        {
            get => _Publisher;
            set { _Publisher = value; change(nameof(Publisher)); }
        }

        /// <summary>
        /// The copyright notice for this software package.
        /// </summary>
        public string Copyright
        {
            get => _Copyright;
            set { _Copyright = value; change(nameof(Copyright)); }
        }

        /// <summary>
        /// An associated website for this software package.
        /// </summary>
        public string Website
        {
            get => _Website;
            set { _Website = value; change(nameof(Website)); }
        }

        /// <summary>
        /// The version of this software package.
        /// </summary>
        public string Version
        {
            get => _Version;
            set { _Version = value; change(nameof(Version)); }
        }

        /// <summary>
        /// The output file format.
        /// </summary>
        public string OutputFilename
        {
            get => _OutputFilename;
            set { _OutputFilename = value; change(nameof(OutputFilename)); }
        }

        /// <summary>
        /// The installer system to use, for example NSIS or WinRar SFX.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public InstallerType Installer
        {
            get => _Installer;
            set { _Installer = value; change(nameof(Installer)); }
        }

        /// <summary>
        /// The framework to bundle along side the software package.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SupportType SupportFramework
        {
            get => _SupportFramework;
            set { _SupportFramework = value; change(nameof(SupportFramework)); }
        }

        /// <summary>
        /// The level of elevation to request for code execution.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RequestExecutionLevelType RequestExecutionLevel
        {
            get => _RequestExecutionLevel;
            set { _RequestExecutionLevel = value; change(nameof(RequestExecutionLevel)); }
        }

        /// <summary>
        /// Weather or not to create a start menu folder for shortcuts.
        /// </summary>
        public bool CreateStartMenuFolder
        {
            get => _CreateStartMenuShortcut;
            set { _CreateStartMenuShortcut = value; change(nameof(CreateStartMenuFolder)); }
        }
        
        /// <summary>
        /// The name of the start menu folder to create.
        /// </summary>
        public string StartMenuFolderName
        {
            get => _StartMenuFolderName;
            set { _StartMenuFolderName = value; change(nameof(StartMenuFolderName)); }
        }
        
        /// <summary>
        /// Weather or not to include a license page on the installer.
        /// </summary>
        public bool IncludeLicensePage
        {
            get => _IncludeLicensePage;
            set { _IncludeLicensePage = value; change(nameof(IncludeLicensePage)); }
        }
        
        /// <summary>
        /// The text of the license to show on the installer.
        /// </summary>
        public string License
        {
            get => _License;
            set { _License = value; change(nameof(License)); }
        }
        
        /// <summary>
        /// The path from which to copy files. If no drive letter is specified, assumes relative.
        /// </summary>
        public string SourcePath
        {
            get => _SourcePath;
            set { _SourcePath = value; change(nameof(SourcePath)); }
        }

        /// <summary>
        /// The equivalent of a .gitignore file, same format.
        /// </summary>
        public string SourceIgnores
        {
            get => _SourceIgnores;
            set { _SourceIgnores = value; change(nameof(SourceIgnores)); }
        }


        public string DesktopShortcutIncludes
        {
            get => _DesktopShortcutIncludes;
            set { _DesktopShortcutIncludes = value; change(nameof(DesktopShortcutIncludes)); }
        }


        public string PrimaryExeFilename
        {
            get => _PrimaryExeFilename;
            set { _PrimaryExeFilename = value; change(nameof(PrimaryExeFilename)); }
        }


        public bool IncludeUninstaller
        {
            get => _IncludeUninstaller;
            set { _IncludeUninstaller = value; change(nameof(IncludeUninstaller)); }
        }
        

        /// <summary>
        /// Any extra code to inject when building the NSIS script.
        /// </summary>
        public string ExtraNsisCode
        {
            get => _ExtraNsisCode;
            set { _ExtraNsisCode = value; change(nameof(ExtraNsisCode)); }
        }


        public string ExtraUninstNsisCode
        {
            get => _ExtraUninstNsisCode;
            set { _ExtraUninstNsisCode = value; change(nameof(ExtraUninstNsisCode)); }
        }


        #endregion


        #region Events


        /// <summary>
        /// Fired when one of these properties change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        void change(string caller) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));


        #endregion


        /// <summary>
        /// Create a new instance of IbProject.
        /// </summary>
        public IbProject()
        {
            this.ProductName = "Untitled";
            this.SoftwareType = SoftwareType.DotNetApplication;
            this.Description = "";
            this.Copyright = "Unknown";
            this.Version = "1.0.0.0";
            this.Installer = InstallerType.NSIS;
            this.SupportFramework = SupportType.None;
            this.RequestExecutionLevel = RequestExecutionLevelType.Admin;
            this.SourcePath = "/";
            this.SourceIgnores = "*.pdb\n*.ibf";
            this.OutputFilename = "{ProductName}_Installer_v{Version}.exe";
            this.IncludeUninstaller = true;
        }


        /// <summary>
        /// Save a project to disk.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveAs(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filename, json);
        }


        /// <summary>
        /// Load a project from disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static IbProject Load(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<IbProject>(json);
        }
    

        /// <summary>
        /// Generates the EXE filename based on the format specified.
        /// </summary>
        public string GenerateExeFilename()
        {
            string output = OutputFilename;
            output = output.Replace("{ProductName}", ProductName);
            output = output.Replace("{Version}", Version);
            output = output.Replace("{RequestExecutionLevel}", RequestExecutionLevel.ToString());
            output = output.Replace("{Publisher}", Publisher);
            if (output.Contains("{Time}"))
            {
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                output = output.Replace("{Time}", ((int)t.TotalSeconds).ToString());
            }
            return output;
        }


        public string GetRoot(string projectFilename)
        {
            FileInfo info = new FileInfo(projectFilename);
            if (string.IsNullOrWhiteSpace(SourcePath) || SourcePath == "/")
            {
                return info.DirectoryName.Replace('\\', '/').TrimEnd('/') + "/";
            }
            else if (Path.IsPathFullyQualified(SourcePath))
            {
                return SourcePath.Replace('\\', '/').TrimEnd('/') + "/";
            }
            else
            {
                string rel_path = info.DirectoryName + SourcePath.Replace('/', '\\');
                if (Directory.Exists(rel_path))
                {
                    return rel_path.Replace('\\', '/').TrimEnd('/') + "/";
                }
                throw new Exception("Unable to determine root path.");
            }
        }


        /// <summary>
        /// Call this to prepare the file structure for building.
        /// </summary>
        /// <param name="projectFilename">The path of the saved project. Throws exception if null or empty.</param>
        /// <returns></returns>
        public IbFileSystem CompileFileStructure(string projectFilename)
        {
            IbFileSystem result = new IbFileSystem();

            if (string.IsNullOrWhiteSpace(projectFilename))
                throw new ArgumentNullException(nameof(projectFilename), "Project must be saved first.");

            // Validate Root Path
            string root = GetRoot(projectFilename);

            // Get the files!
            if (Directory.Exists(root))
            {
                var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories).Select(t => t.Replace('\\', '/')).OrderBy(t => t).ToArray();
                var directories = Directory.GetDirectories(root, "*", SearchOption.AllDirectories).Select(t => t.Replace('\\', '/')).OrderBy(t => t).ToArray();

                // Only prepare stuff if we have files and folders to filter through.
                if (files.Length + directories.Length > 0)
                {
                    // test each file/folder.
                    var ignoreFolders = new List<string>();
                    var validator = new IbValidator(SourceIgnores);

                    foreach (string path in directories)
                    {
                        string rel_path = path.Replace(root, "");
                        if (!validator.ShouldIgnore(rel_path))
                        {
                            result.Add(IbFileSystemEntry.Create(rel_path, path, true));
                        }
                        else
                        {
                            ignoreFolders.Add(rel_path);
                        }
                    }

                    foreach (string path in files)
                    {
                        string rel_path = path.Replace(root, "");
                        if (!validator.ShouldIgnore(rel_path) && !ignoreFolders.Any(f => rel_path.StartsWith(f)))
                        {
                            result.Add(IbFileSystemEntry.Create(rel_path, path, false));
                        }
                    }
                }
            }

            // Return the results.
            return result;
        }
        


        internal IbFactory PrepareFactory(string sourcePath, string filename)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !Directory.Exists(sourcePath))
            {
                throw new InstallerFactoryException("Please save the project before attempting to build it.");
            }

            IbFactory factory = null;
            DirectoryInfo srcDirectory = new DirectoryInfo(sourcePath);
            switch (Installer)
            {
                case InstallerType.NSIS:
                    factory = new NsisFactory(srcDirectory, this);
                    break;
            }

            return factory;
        }
    }
}