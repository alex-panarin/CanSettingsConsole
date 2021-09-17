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
            var payload = GetPayload(bytes);
            switch ((ControllerType)payload.Type)
            {
                case ControllerType.Display:
                    return new DisplayController()
                    {
                        Sector = (byte)payload.Sector,
                        Code = (uint)payload.Code,
                        Status = (byte)payload.Status
                        
                    };
                case ControllerType.Main:
                    return new MainController()
                    {
                        Sector = (byte)payload.Sector,
                        Code = (uint)payload.Code,
                        Status = (byte)payload.Status
                    };
                case ControllerType.Translate:
                    return new TranslateController()
                    {
                        Sector = (byte)payload.Sector,
                        Code = (uint)payload.Code,
                        Status = (byte)payload.Status,
                        Level = (byte)payload.Sector
                    };
            }

            return null;
        }

        private SerialPortPayload GetPayload(byte[] bytes)
        {
            var res = Convert.ToUInt64(Encoding.GetEncoding(1251).GetString(bytes));
            return (SerialPortPayload)res;
        }
    }

    
}
