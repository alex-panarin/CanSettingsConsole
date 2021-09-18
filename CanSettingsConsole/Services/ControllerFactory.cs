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
            var payload = GetPayload(result);
            switch ((ControllerType)payload.Type)
            {
                case ControllerType.Display:
                    return new DisplayController()
                    {
                        Sector = payload.Sector,
                        Code = payload.Code,
                        Status = payload.Status
                        
                    };
                case ControllerType.Main:
                    return new MainController()
                    {
                        Sector = payload.Sector,
                        Code = payload.Code,
                        Status = payload.Status
                    };
                case ControllerType.Translate:
                    return new TranslateController()
                    {
                        Sector = payload.Sector,
                        Code = payload.Code,
                        Status = payload.Status,
                        Level = payload.Sector
                    };
            }

            return null;
        }

        private SerialPortMessage GetPayload(string result)
        {
            return SerialPortMessage.FromString(result);
        }
    }

    
}
