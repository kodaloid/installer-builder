using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace InstallerBuilder.Controls
{
    /// <summary>
    /// Interaction logic for ComboAndLabel.xaml
    /// </summary>
    public partial class ComboAndLabel : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(ComboAndLabel), new PropertyMetadata("Input Title"));
        public static readonly DependencyProperty LabelDockProperty = DependencyProperty.Register("LabelDock", typeof(Dock), typeof(ComboAndLabel), new PropertyMetadata(Dock.Left));
        public static readonly DependencyProperty LabelAlignProperty = DependencyProperty.Register("LabelAlign", typeof(TextAlignment), typeof(ComboAndLabel), new PropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboAndLabel), new PropertyMetadata(null));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ComboAndLabel), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowCheckBoxProperty = DependencyProperty.Register("ShowCheckBox", typeof(bool), typeof(ComboAndLabel), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowButtonProperty = DependencyProperty.Register("ShowButton", typeof(bool), typeof(ComboAndLabel), new PropertyMetadata(false));
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ComboAndLabel), new PropertyMetadata(null));


        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }
        public Dock LabelDock { get { return (Dock)GetValue(LabelDockProperty); } set { SetValue(LabelDockProperty, value); } }
        public TextAlignment LabelAlign { get { return (TextAlignment)GetValue(LabelAlignProperty); } set { SetValue(LabelAlignProperty, value); } }
        public object SelectedItem { get { return GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); } }
        public IEnumerable ItemsSource { get { return (IEnumerable)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }
        public bool ShowCheckBox { get { return (bool)GetValue(ShowCheckBoxProperty); } set { SetValue(ShowCheckBoxProperty, value); } }
        public bool ShowButton { get { return (bool)GetValue(ShowButtonProperty); } set { SetValue(ShowButtonProperty, value); } }
        public DataTemplate ItemTemplate { get { return (DataTemplate)GetValue(ItemTemplateProperty); } set { SetValue(ItemTemplateProperty, value); } }


        public ComboAndLabel()
        {
            InitializeComponent();            
        }
    }
}