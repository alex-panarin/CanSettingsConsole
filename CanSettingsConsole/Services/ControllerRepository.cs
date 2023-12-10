using CanSettingsConsole.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace CanSettingsConsole.Services
{
    public class ControllerRepository
        : IControllerRepository
    {
        const string fileName = "display.dt";
        public DisplayController GetTemplateData()
        {
            var path = Path.Combine(Environment.CurrentDirectory, fileName);
            if (!File.Exists(path))
                return null;
            var data = File.ReadAllText(path);
            return JsonSerializer.Deserialize<DisplayController>(data, new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                PropertyNameCaseInsensitive = true
            });
        }

        public void SaveTemplateData(DisplayController model, bool showMessage)
        {
            try
            {
                var data = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    IgnoreReadOnlyProperties = true,
                    PropertyNameCaseInsensitive = true
                });

                var path = Path.Combine(Environment.CurrentDirectory, fileName);
                File.WriteAllText(path, data);
                if(showMessage)
                    MessageBox.Show("Шаблон данных успешно сохранен");
            }
            catch (Exception x)
            {
                MessageBox.Show($"Не смогли сохранить шаблон => {x.Message}"); 
            }
        }
    }
}
