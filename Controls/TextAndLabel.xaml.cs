using System.Windows;
using System.Windows.Controls;

namespace InstallerBuilder.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TextAndLabel : UserControl
    {
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent("ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextAndLabel));
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent("Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextAndLabel));


        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TextAndLabel), new PropertyMetadata("Input Title"));
        public static readonly DependencyProperty LabelDockProperty = DependencyProperty.Register("LabelDock", typeof(Dock), typeof(TextAndLabel), new PropertyMetadata(Dock.Left));
        public static readonly DependencyProperty LabelAlignProperty = DependencyProperty.Register("LabelAlign", typeof(TextAlignment), typeof(TextAndLabel), new PropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextAndLabel), new PropertyMetadata(""));
        public static readonly DependencyProperty ShowCheckBoxProperty = DependencyProperty.Register("ShowCheckBox", typeof(bool), typeof(TextAndLabel), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowButtonProperty = DependencyProperty.Register("ShowButton", typeof(bool), typeof(TextAndLabel), new PropertyMetadata(false));
        public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(TextAndLabel), new PropertyMetadata(false));


        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }
        public Dock LabelDock { get { return (Dock)GetValue(LabelDockProperty); } set { SetValue(LabelDockProperty, value); } }
        public TextAlignment LabelAlign { get { return (TextAlignment)GetValue(LabelAlignProperty); } set { SetValue(LabelAlignProperty, value); } }
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public bool ShowCheckBox { get { return (bool)GetValue(ShowCheckBoxProperty); } set { SetValue(ShowCheckBoxProperty, value); } }
        public bool ShowButton { get { return (bool)GetValue(ShowButtonProperty); } set { SetValue(ShowButtonProperty, value); } }
        public bool Checked { get { return (bool)GetValue(CheckedProperty); } set { SetValue(CheckedProperty, value); } }


        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }


        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }


        public TextAndLabel()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }
    }
}