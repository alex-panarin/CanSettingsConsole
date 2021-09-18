using System;
using System.Text;
using System.Windows.Navigation;
using CanSettingsConsole.Models;

namespace CanSettingsConsole.Services
{
    public interface IControllerFactory
    {
        ControllerBase Create(byte[] bytes);
    }
    public class ControllerFactory : IControllerFactory
    {
        public ControllerBase Create(byte[] bytes)
        {
            var res = Encoding.ASCII.GetString(bytes);
            return Create(res);
        }
        public ControllerBase Create(string result)
        {
            var payload = new SerialPortMessage(result);
            switch ((ControllerType)payload.Type)
            {
                case ControllerType.Display:
                    return CreateController<DisplayController>(payload);
                case ControllerType.Main:
                    return CreateController<MainController>(payload);
                case ControllerType.Translate:
                    return CreateController<TranslateController>(payload);
            }

            return null;
        }

        private T CreateController<T>(SerialPortMessage payload)
            where T : ControllerBase, new()
        {
            return new T
            {
                Sector = payload.Sector,
                Code = payload.Code,
                Status = payload.Status
            };
        }
    }
}
