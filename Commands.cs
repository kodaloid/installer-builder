using System;
using System.Windows.Input;

namespace InstallerBuilder
{
    public static class Commands
    {
        private static Type ownerType = typeof(MainWindow);
        private static RoutedUICommand GenCommand(string text, string name) => new RoutedUICommand(text, name, ownerType);


        public static readonly RoutedUICommand FileNew = GenCommand("New", nameof(FileNew));
        public static readonly RoutedUICommand FileOpen = GenCommand("_Open..", nameof(FileOpen));
        public static readonly RoutedUICommand FileSave = GenCommand("_Save", nameof(FileSave));
        public static readonly RoutedUICommand FileSaveAs = GenCommand("Save _As..", nameof(FileSaveAs));
        public static readonly RoutedUICommand FileExit = GenCommand("E_xit", nameof(FileExit));


        public static readonly RoutedUICommand BuildPreview = GenCommand("_Build Preview", nameof(BuildPreview));
        public static readonly RoutedUICommand BuildNow = GenCommand("_Build Now...", nameof(BuildNow));
        public static readonly RoutedUICommand About = GenCommand("About", nameof(About));
    }
}