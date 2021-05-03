using System.Windows;
using System.Windows.Controls;

namespace InstallerBuilder.Controls
{
    /// <summary>
    /// Interaction logic for FormElement.xaml
    /// </summary>
    public partial class FormElement : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(FormElement), new PropertyMetadata("Input Title"));
        public static readonly DependencyProperty LabelDockProperty = DependencyProperty.Register("LabelDock", typeof(Dock), typeof(FormElement), new PropertyMetadata(Dock.Left));
        public static readonly DependencyProperty LabelAlignProperty = DependencyProperty.Register("LabelAlign", typeof(TextAlignment), typeof(FormElement), new PropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty ShowCheckBoxProperty = DependencyProperty.Register("ShowCheckBox", typeof(bool), typeof(TextAreaAndLabel), new PropertyMetadata(false));


        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }
        public Dock LabelDock { get { return (Dock)GetValue(LabelDockProperty); } set { SetValue(LabelDockProperty, value); } }
        public TextAlignment LabelAlign { get { return (TextAlignment)GetValue(LabelAlignProperty); } set { SetValue(LabelAlignProperty, value); } }
        public bool ShowCheckBox { get { return (bool)GetValue(ShowCheckBoxProperty); } set { SetValue(ShowCheckBoxProperty, value); } }


        public FormElement()
        {
            InitializeComponent();
        }
    }
}