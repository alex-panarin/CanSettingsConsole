using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace CanSettingsConsole.UI
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return GetDescription((Enum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            var value = type.GetMember(en.ToString())
                .Select(m => (DescriptionAttribute) m.GetCustomAttribute(typeof(DescriptionAttribute), false))
                .FirstOrDefault(m => m != null);
             
            return value != null ? value.Description :  en.ToString();
        }
    }
}
