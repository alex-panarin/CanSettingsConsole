using CanSettingsConsole.Models;

namespace CanSettingsConsole.Services
{
    public interface IControllerRepository
    {
        DisplayController GetTemplateData();
        void SaveTemplateData(DisplayController model);
    }
}