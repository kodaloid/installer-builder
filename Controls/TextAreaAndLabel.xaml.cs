using System.Windows;
using System.Windows.Controls;

namespace InstallerBuilder.Controls
{
    /// <summary>
    /// Interaction logic for TextAreaAndLabel.xaml
    /// </summary>
    public partial class TextAreaAndLabel : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TextAreaAndLabel), new PropertyMetadata("Input Title"));
        public static readonly DependencyProperty LabelDockProperty = DependencyProperty.Register("LabelDock", typeof(Dock), typeof(TextAreaAndLabel), new PropertyMetadata(Dock.Left));
        public static readonly DependencyProperty LabelAlignProperty = DependencyProperty.Register("LabelAlign", typeof(TextAlignment), typeof(TextAreaAndLabel), new PropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextAreaAndLabel), new PropertyMetadata(""));
        public static readonly DependencyProperty TextHeightProperty = DependencyProperty.Register("TextHeight", typeof(double), typeof(TextAreaAndLabel), new PropertyMetadata(28 * 3.0));
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(TextAreaAndLabel), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowCheckBoxProperty = DependencyProperty.Register("ShowCheckBox", typeof(bool), typeof(TextAreaAndLabel), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowButtonProperty = DependencyProperty.Register("ShowButton", typeof(bool), typeof(TextAreaAndLabel), new PropertyMetadata(false));


        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }
        public Dock LabelDock { get { return (Dock)GetValue(LabelDockProperty); } set { SetValue(LabelDockProperty, value); } }
        public TextAlignment LabelAlign { get { return (TextAlignment)GetValue(LabelAlignProperty); } set { SetValue(LabelAlignProperty, value); } }
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public double TextHeight { get { return (double)GetValue(TextHeightProperty); } set { SetValue(TextHeightProperty, value); } }
        public bool AcceptsReturn { get { return (bool)GetValue(AcceptsReturnProperty); } set { SetValue(AcceptsReturnProperty, value); } }
        public bool ShowCheckBox { get { return (bool)GetValue(ShowCheckBoxProperty); } set { SetValue(ShowCheckBoxProperty, value); } }
        public bool ShowButton { get { return (bool)GetValue(ShowButtonProperty); } set { SetValue(ShowButtonProperty, value); } }


        public TextAreaAndLabel()
        {
            InitializeComponent();
        }
    }
}