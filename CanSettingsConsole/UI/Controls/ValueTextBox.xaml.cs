using CanSettingsConsole.Core;
using CanSettingsConsole2.Services;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanSettingsConsole2.UI.Controls
{
    /// <summary>
    /// Interaction logic for ValueTextBox.xaml
    /// </summary>
    public partial class ValueTextBox : UserControl
    {
        private bool _suppressMaskUpdate = false;
        private MessageContainer _messageContainer;
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
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
          "IsChecked", typeof(bool), typeof(ValueTextBox),
          new FrameworkPropertyMetadata(default(bool), IsCheckedChange)
          {
              BindsTwoWayByDefault = true
          });
        private static void IsCheckedChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var valueTextBox = dependencyObject as ValueTextBox;
            var value = (bool?)e.NewValue;
            valueTextBox._IsChecked.IsChecked = value == true;
        }
        public ValueTextBox()
        {
            InitializeComponent();
            _messageContainer = MessageContainer.Instance;
            _messageContainer.RegisterMessage(MessageContainer.PLUS_MESSAGE, OnPlus);
            _messageContainer.RegisterMessage(MessageContainer.MINUS_MESSAGE, OnMinus);
           
        }
        private void OnMinus(object obj)
        {
            if (IsChecked == false) return;
            MinusValue();
        }

        private void OnPlus(object obj)
        {
            if (IsChecked == false) return;
            PlusValue();
        }

        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (sender == _ButtonDn)
            {
                MinusValue();
            }
            if (sender == _ButtonUp)
            {
                PlusValue();
            }
        }
       
        private void MinusValue()
        {
            if (int.TryParse(TextValue, out var value))
                TextValue = (value - 1).ToString();
        }
        private void PlusValue()
        {
            if (int.TryParse(TextValue, out var value))
                TextValue = (value + 1).ToString();
        }
    }
}
