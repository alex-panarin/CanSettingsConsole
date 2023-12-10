using CanSettingsConsole.Core;
using System.Windows;
using System.Windows.Controls;

namespace CanSettingsConsole2.UI.Controls
{
    /// <summary>
    /// Interaction logic for ValueTextBox.xaml
    /// </summary>
    public partial class ValueTextBox : UserControl
    {
        private bool _suppressMaskUpdate = false;

        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register(
           "TextValue", typeof(string), typeof(ValueTextBox),
           new FrameworkPropertyMetadata(default(string), TextValueChange)
           {
               BindsTwoWayByDefault = true
           });

        private static void TextValueChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var valueTextBox = dependencyObject as ValueTextBox;
            var text = e.NewValue as string;
            if (text != null && valueTextBox != null)
            {
                valueTextBox._suppressMaskUpdate = true;
                valueTextBox._Text.Text = text;
                valueTextBox._suppressMaskUpdate = false;
            }
        }

        public ValueTextBox()
        {
            InitializeComponent();
        }
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (sender == _ButtonDn)
            {
                if(int.TryParse(TextValue, out var value))
                    TextValue = (value - 1).ToString();
            }
            if (sender == _ButtonUp)
            {
                if (int.TryParse(TextValue, out var value))
                    TextValue = (value + 1).ToString();
            }
        }
    }
}
