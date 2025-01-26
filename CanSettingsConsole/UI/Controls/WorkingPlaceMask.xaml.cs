using CanSettingsConsole.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CanSettingsConsole.UI.Controls
{
    /// <summary>
    /// Interaction logic for WorkingPlaceMask.xaml
    /// </summary>
    public partial class WorkingPlaceMask : UserControl
    {
        private bool _suppressMaskUpdate = false; 
        private List<CheckBox> _segments = new List<CheckBox>();
        public WorkingPlaceMask()
        {
            InitializeComponent();
            _segments.Add(One);
            _segments.Add(Two);
            _segments.Add(Three);
            _segments.Add(Four);
            _segments.Add(Five);
            _segments.Add(Six);
            _segments.Add(Seven);
            _segments.Add(Eight);
        }
        public static readonly DependencyProperty MaskProperty = DependencyProperty.Register(
            "Mask", typeof(string), typeof(WorkingPlaceMask),
            new FrameworkPropertyMetadata(default(string), MaskChanged)
            {
                BindsTwoWayByDefault = true
            });

        private static void MaskChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is string text 
                && dependencyObject is WorkingPlaceMask maskBox)
            {
                maskBox._suppressMaskUpdate = true;
                var i = 0;
                foreach (var segment in text.Split('.'))
                {
                    maskBox._segments[i].IsChecked = int.Parse(segment) == 1 ? true : false;
                    if(maskBox._segments[i].Command == null)
                        maskBox._segments[i].Command = new WindowCommand(maskBox.OnCheckChanged);
                    i++;
                }
                maskBox._suppressMaskUpdate = false;
            }
        }
        public string Mask
        {
            get { return (string)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        private void OnCheckChanged(object checkBox)
        {
            if (_suppressMaskUpdate)
                return;

            var box = (CheckBox)checkBox;
            switch(box.Name)
            {
                case "One":
                    SetMaskBit(0, box.IsChecked);
                    break;
                case "Two":
                    SetMaskBit(1, box.IsChecked);
                    break;
                case "Three":
                    SetMaskBit(2, box.IsChecked);
                    break;
                case "Four":
                    SetMaskBit(3, box.IsChecked);
                    break;
                case "Five":
                    SetMaskBit(4, box.IsChecked);
                    break;
                case "Six":
                    SetMaskBit(5, box.IsChecked);
                    break;
                case "Seven":
                    SetMaskBit(6, box.IsChecked);
                    break;
                case "Eight":
                    SetMaskBit(7, box.IsChecked);
                    break;

            }
        }

        private void SetMaskBit(int bit, bool? val)
        {
            var masks = Mask.Split('.');
            masks[bit] = val == true ? "1" : "0";
            Mask = string.Join(".", masks);
                
        }
    }
}
