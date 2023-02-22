using CanSettingsConsole.Models;
using CanSettingsConsole.Wrappers;
using System;
using System.ComponentModel;
using System.Text;

namespace CanSettingsConsole.Services
{
    public enum ControllerType
    {
        None = 0,
        Main = 1,
        Translate = 2,
        Display = 3
    };

    public enum ControllerStatus : byte
    {
        [Description("Не определен")]
        None,
        [Description("Готов к работе")]
        Ready,
        [Description("Занят, обратитесь позже")]
        Busy
    };
    
    public enum ControllerCommand : byte
    {
        Test,
        Status,
        Set,
        Get = 4,
        None = 5,
        Clear = 6
    };
    public interface IControllerFactory
    {
        ControllerWrapper CreateController(byte[] bytes);
        string Get();
        string Post(ControllerBase controller);

    }
    public class ControllerFactory : IControllerFactory
    {
        private ControllerWrapper CreateInitializeController(ControllerType type, string[] values)
        {
            ControllerWrapper wrapper = null;

            switch (type)
            {
                case ControllerType.Main:
                    wrapper = new MainWrapper(new MainController());
                    break;
                case ControllerType.Translate:
                    wrapper = new TranslateWrapper(new TranslateController());
                    break;
                case ControllerType.Display:
                    wrapper = new DisplayWrapper(new DisplayController(), 
                        new ControllerRepository());
                    break;
            }
            
            wrapper?.Model?.Initialize(values);
            return wrapper;
        }
        public ControllerWrapper CreateController(byte[] bytes)
        {
            var result = Encoding.ASCII.GetString(bytes);
            var values = result.Split('|');
            return CreateInitializeController((ControllerType)Convert.ToByte(values[1]), values);
        }
        public string Get() =>  $"{(byte)ControllerCommand.Get}|{(byte)ControllerType.None}\r";
        public string Post(ControllerBase controller) => $"{(byte)ControllerCommand.Set}|{controller}\r";
    }
}