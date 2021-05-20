using InstallerBuilder.Includes.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace InstallerBuilder.Includes.Factories
{
    public sealed class NsisFactory : IbFactory
    {
        StringBuilder sb;
        Process compiler;


        public NsisFactory(DirectoryInfo sourceDirectory, IbProject project) : base("NSIS Installer", sourceDirectory, project)
        {
            sb = new StringBuilder();
        }


        public override void Begin(string outputFilename, string buildFolder, IbFileSystem fileSystem)
        {
            try
            {
                // Create the nsi script that NSIS will read for instructions.
                string nsi = BuildNsiScripts(outputFilename, buildFolder, fileSystem);
                string file = $"{buildFolder.TrimEnd('\\')}\\_temp.nsi";

                // save the script.
                if (File.Exists(file)) File.Delete(file);
                File.WriteAllText(file, nsi);


                // run nsis.
                compiler = new Process();
                //compiler.StartInfo.FileName = "\"C:\\Program Files (x86)\\NSIS\\makensis.exe\"";
                compiler.StartInfo.FileName = FileHelper.ProgramFilesx86() + "\\NSIS\\makensis.exe";
                compiler.StartInfo.CreateNoWindow = false;
                compiler.StartInfo.Arguments = $"\"{file}\"";
                compiler.StartInfo.UseShellExecute = false;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.StartInfo.RedirectStandardError = true;
                compiler.EnableRaisingEvents = true;
                compiler.OutputDataReceived += Compiler_OutputDataReceived;
                compiler.ErrorDataReceived += Compiler_ErrorDataReceived;
                compiler.Exited += Compiler_Exited;
                compiler.Start();
                compiler.BeginErrorReadLine();
                compiler.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Compiler_Exited(object sender, EventArgs e)
        {
            try
            {
                compiler.OutputDataReceived -= Compiler_OutputDataReceived;
                compiler.ErrorDataReceived -= Compiler_ErrorDataReceived;
                compiler.Exited -= Compiler_Exited;
                compiler.Dispose();
            }
            catch { }
            DoComplete();
        }


        private void Compiler_ErrorDataReceived(object sender, DataReceivedEventArgs e) => DoLogEvent(e.Data);


        private void Compiler_OutputDataReceived(object sender, DataReceivedEventArgs e) => DoLogEvent(e.Data);


        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Project.ProductName))
                throw new InstallerFactoryException("Product name can not be empty.");

            if (string.IsNullOrWhiteSpace(Project.Publisher))
                throw new InstallerFactoryException("Publisher name can not be empty.");

            /* foreach (var folder in Project.Folders)
            {
                if (folder.Files.Count == 0)
                {
                    throw new InstallerFactoryException("Folders can not be empty.");
                }
                else if (!folder.Files.All(t => t.Path.StartsWith(SourceDirectory.FullName)))
                {
                    throw new InstallerFactoryException("One or more folders reference files outside the source location.");
                }
            } */
        }


        private string BuildNsiScripts(string outputFilename, string buildFolder, IbFileSystem fileSystem)
        {
            // Initializers
            sb.AppendLine("!include \"MUI2.nsh\"");
            sb.AppendLine(";NSIS Build Script");
            sb.AppendLine(";Compiled by Kodaloids Installer Builder v1.0");
            sb.AppendLine("");
            sb.AppendLine("; --------------------------------");
            sb.AppendLine("; Include Modern UI");


            // General Setup
            AppendHeader("General");
            sb.AppendLine($"Name \"{Project.ProductName}\"");
            sb.AppendLine($"OutFile \"{outputFilename}\"");
            sb.AppendLine($"InstallDir \"$PROGRAMFILES\\{Project.ProductName}\"");
            sb.AppendLine($"InstallDirRegKey HKCU \"Software\\{Project.Publisher}\\{Project.ProductName}\" \"\"");
            sb.AppendLine($"RequestExecutionLevel {Project.RequestExecutionLevel.ToString().ToLower()}");
            sb.AppendLine("");
            sb.AppendLine("; --------------------------------");
            sb.AppendLine("; Interface Settings");
            sb.AppendLine("!define MUI_ABORTWARNING");
            sb.AppendLine($"!define SOURCEPATH \"{buildFolder}\"");


            // Pages
            AppendHeader("Pages");
            if (Project.IncludeLicensePage) sb.AppendLine("!insertmacro MUI_PAGE_LICENSE \"License.txt\"");
            sb.AppendLine("!insertmacro MUI_PAGE_COMPONENTS");
            sb.AppendLine("!insertmacro MUI_PAGE_DIRECTORY");
            sb.AppendLine("!insertmacro MUI_PAGE_INSTFILES");
            sb.AppendLine("");
            sb.AppendLine("!insertmacro MUI_UNPAGE_CONFIRM");
            sb.AppendLine("!insertmacro MUI_UNPAGE_INSTFILES");


            // Build the installer sections
            AppendHeader("Installer Sections");


            // Copy the files.
            string last_directory = "";
            bool addedPath = false;


            // The main section.
            sb.AppendLine($"Section \"Application Files\" SecDummy");
            sb.AppendLine($"  WriteRegStr HKCU \"Software\\{Project.Publisher}\\{Project.ProductNameNoSpaces}\" \"\" $INSTDIR");
            sb.AppendLine($"  WriteRegStr HKLM \"Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{Project.ProductNameNoSpaces}\" \\ ");
            sb.AppendLine($"                   \"DisplayName\" \"{Project.ProductName}\" ");
            sb.AppendLine($"  WriteRegStr HKLM \"Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{Project.ProductNameNoSpaces}\" \\ ");
            sb.AppendLine($"                   \"UninstallString\" \"$INSTDIR\\Uninstall.exe\"");
            sb.AppendLine($"  WriteRegStr HKLM \"Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{Project.ProductNameNoSpaces}\" \\ ");
            sb.AppendLine($"                   \"DisplayIcon\" \"$INSTDIR\\{Project.PrimaryExeFilename}\"");

            sb.AppendLine($"  WriteUninstaller \"$INSTDIR\\Uninstall.exe\"");
            foreach (IbFileSystemEntry entry in fileSystem)
            {
                string path = entry.SourceRelativePath.Replace('/', '\\');
                if (!entry.IsFolder)
                {
                    string entry_directory = entry.SourceRelativePathParent.Replace('/', '\\');
                    if (entry_directory != last_directory)
                    {
                        last_directory = entry_directory;
                        sb.AppendLine($"  SetOutPath \"$INSTDIR\\{entry_directory}\"");
                        addedPath = true;
                    }
                    else if (addedPath == false)
                    {
                        sb.AppendLine($"  SetOutPath \"$INSTDIR\"");
                        addedPath = true;
                    }

                    sb.AppendLine("  File \"${SOURCEPATH}\\" + path + "\"");
                }
            }

            List<string> linkFiles = new List<string>();

            if (!string.IsNullOrWhiteSpace(Project.DesktopShortcutIncludes))
            {
                string[] desktopShortcutNames = Project.DesktopShortcutIncludes.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                foreach (string desktopShortcutName in desktopShortcutNames)
                {
                    string linkFile = (desktopShortcutName.Contains("/") || desktopShortcutName.Contains("/")
                                    ? LastPathPart(desktopShortcutName)
                                    : desktopShortcutName).Replace(".exe", "", StringComparison.InvariantCultureIgnoreCase);

                    linkFiles.Add(linkFile);
                    sb.AppendLine($"  CreateShortcut \"$DESKTOP\\{linkFile}.lnk\" \"$INSTDIR\\{desktopShortcutName}\"");
                }
            }

            sb.AppendLine("  WriteUninstaller \"$INSTDIR\\Uninstall.exe\"");
            sb.AppendLine("SectionEnd");


            // Language Setup + Run after install option
            AppendHeader("Languages");
            sb.AppendLine($"!define MUI_FINISHPAGE_RUN \"$INSTDIR\\{Project.PrimaryExeFilename}\"");
            sb.AppendLine("!insertmacro MUI_PAGE_FINISH");
            sb.AppendLine("!insertmacro MUI_LANGUAGE \"English\"");


            // Version Info
            AppendHeader("Version Info");
           // sb.AppendLine("LoadLanguageFile \"${NSISDIR}\\Contrib\\Language files\\English.nlf\"");
            sb.AppendLine("VIProductVersion \"" + Project.Version + "\"");
            sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"ProductName\" \"" + Project.ProductName + "\"");
            //sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"Comments\" \"A test comment\"");
            sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"CompanyName\" \"" + Project.Publisher + "\"");
            //sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"LegalTrademarks\" \"Test Application is a trademark of Fake company\"");
            sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"LegalCopyright\" \"" + Project.Copyright + "\"");
            sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"FileDescription\" \"" + Project.Description + "\"");
            sb.AppendLine("VIAddVersionKey /LANG=${LANG_ENGLISH} \"FileVersion\" \"" + Project.Version + "\"");


            // Descriptions & Translations
            AppendHeader("Descriptions");
            sb.AppendLine("LangString DESC_SecDummy ${LANG_ENGLISH} \"The main files and executables required for running the software.\"");
            sb.AppendLine("");
            sb.AppendLine("; Assign language strings to sections");
            sb.AppendLine("!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN");
            sb.AppendLine("!insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)");
            sb.AppendLine("!insertmacro MUI_FUNCTION_DESCRIPTION_END");


            // Uninstaller section
            AppendHeader("Uninstaller Section");
            sb.AppendLine($"Section \"Uninstall\"");
            sb.AppendLine("  Delete \"$INSTDIR\\Uninstall.exe\"");
            foreach (IbFileSystemEntry entry in fileSystem)
            {
                if (!entry.IsFolder)
                {
                    string path = entry.SourceRelativePath.Replace('/', '\\');
                    sb.AppendLine("  Delete \"$INSTDIR\\" + path + "\"");
                }
            }

            foreach (string linkFile in linkFiles)
            {
                if (!string.IsNullOrWhiteSpace(linkFile))
                {
                    sb.AppendLine($"  Delete \"$DESKTOP\\{linkFile}.lnk\"");
                }
            }
            
            sb.AppendLine("  RMDir \"$INSTDIR\"");
            sb.AppendLine($"  DeleteRegKey /ifempty HKCU \"Software\\{Project.Publisher}\\{Project.ProductName}\"");
            sb.AppendLine($"  DeleteRegKey HKLM \"Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{Project.ProductNameNoSpaces}\"");
            sb.AppendLine("SectionEnd");


            // Return the nsi script.
            return sb.ToString();
        }


        private void AppendHeader(string headerText)
        {
            sb.AppendLine("");
            sb.AppendLine("; --------------------------------");
            sb.AppendLine("; " + headerText);
        }


        private static string LastPathPart(string path)
        {
            if (!path.Contains('/')) return path;
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }
    }
}