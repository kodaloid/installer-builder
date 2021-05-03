using System;
using System.IO;
using System.Threading.Tasks;

namespace InstallerBuilder.Includes.Helpers
{
    public static class FileHelper
    {
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }


        public static async Task CopyAsync(string sourceFile, string destFile)
        {
            using (FileStream SourceStream = File.Open(sourceFile, FileMode.Open))
            {
                using (FileStream DestinationStream = File.Create(destFile))
                {
                    await SourceStream.CopyToAsync(DestinationStream);
                }
            }
        }


        public static string ProgramFilesx86()
        {
            if (Environment.Is64BitOperatingSystem || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}