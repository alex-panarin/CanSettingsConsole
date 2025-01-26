using CanSettingsConsole.Wrappers;
using System.Windows;
using System.Windows.Controls;

namespace CanSettingsConsole.UI
{
    public class ConnectionViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TranslateDataTemplate { get; set; }
        public DataTemplate DisplayDataTemplate { get; set; }
        public DataTemplate MainDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                var type = item.GetType();
                if (type == typeof(DisplayWrapper))
                    return DisplayDataTemplate;
                if (type == typeof(TranslateWrapper))
                    return TranslateDataTemplate;
                if (type == typeof(MainWrapper))
                    return MainDataTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
