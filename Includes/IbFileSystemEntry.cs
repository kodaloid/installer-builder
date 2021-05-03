using System;
using System.Collections.Generic;
using System.Linq;

namespace InstallerBuilder.Includes
{
    public sealed class IbFileSystemEntry
    {
        public string Name { get; set; }
        public string SourceRelativePath { get; set; }
        public string SourceRelativePathParent { get => SourceRelativePath.Substring(0, SourceRelativePath.Length - Name.Length); }
        public string SourceAbsolutePath { get; set; }
        public bool IsFolder { get; set; }
        public bool IsShortcut { get; set; }
        
        public IbFileSystemEntry()
        {
            this.Name = "Untitled";
        }

        internal static IbFileSystemEntry Create(string rel_path, string abs_path, bool isFolder)
        {
            return new IbFileSystemEntry
            {
                Name = LastPathPart(rel_path),
                SourceRelativePath = rel_path,
                SourceAbsolutePath = abs_path,
                IsFolder = isFolder
            };
        }


        private static string LastPathPart(string path)
        {
            if (!path.Contains('/')) return path;
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}