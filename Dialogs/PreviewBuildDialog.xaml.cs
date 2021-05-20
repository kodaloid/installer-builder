using InstallerBuilder.Includes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace InstallerBuilder.Dialogs
{
    /// <summary>
    /// Interaction logic for PreviewBuildDialog.xaml
    /// </summary>
    public partial class PreviewBuildDialog : Window
    {
        public PreviewBuildDialog()
        {

        }


        public PreviewBuildDialog(IbProject project, string projectFilePath)
        {
            InitializeComponent();
            try
            {

                var structure = project.CompileFileStructure(projectFilePath);
                var result = new PreviewItemRoot();

                foreach (var item in structure.Where(t => t.IsFolder))
                {
                    result.InsertFile(item);
                }

                foreach (var item in structure.Where(t => !t.IsFolder))
                {
                    result.InsertFile(item);
                }

                treePreview.ItemsSource = result.Items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


    public class PreviewItemRoot : PreviewItem
    {
        internal void InsertFile(IbFileSystemEntry item)
        {
            string[] parts = item.SourceRelativePath.Split('/');
            PreviewItem root = this;
            int index = 0, lastInex = parts.Length - 1;
            foreach (string part in parts)
            {
                if (index == lastInex)
                {
                    root.Items.Add(new PreviewItem { Title = part, IsFolder = false });
                }
                else
                {
                    PreviewItem sub = root[part];
                    if (sub == null)
                    {
                        sub = new PreviewItem { Title = part, IsFolder = true };
                        root.Items.Add(sub);
                    }
                    root = sub;
                }
                index++;
            }
        }
    }


    public class PreviewItem
    {
        public string Title { get; set; }
        public bool IsFolder { get; set; }
        public List<PreviewItem> Items { get; set; }

        public PreviewItem this[string title]
        {
            get { return Items.FirstOrDefault(t => t.Title == title); }
        }

        public PreviewItem()
        {
            this.Items = new List<PreviewItem>();
        }

        public PreviewItem(string title, bool folder)
        {
            this.Title = title;
            this.IsFolder = folder;
            this.Items = new List<PreviewItem>();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}