using System.Windows;

namespace InstallerBuilder.Dialogs
{
    /// <summary>
    /// Interaction logic for TextBoxDialog.xaml
    /// </summary>
    public partial class TextBoxDialog : Window
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TextBoxDialog), new PropertyMetadata("Input Title"));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBoxDialog), new PropertyMetadata("Input Title"));

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }


        public TextBoxDialog(string label, string text, Window owner)
        {
            this.DataContext = this;
            this.InitializeComponent();
            this.Label = label;
            this.Text = text;
            this.Owner = owner;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.tb1.Focus();
            this.tb1.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}