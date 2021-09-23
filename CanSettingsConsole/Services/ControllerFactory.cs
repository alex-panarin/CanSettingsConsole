using System;
using System.ComponentModel;
using System.Dynamic;
using System.Text;
using System.Windows.Markup.Localizer;
using System.Windows.Navigation;
using CanSettingsConsole.Models;
using CanSettingsConsole.Wrappers;

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
        None,
        Status,
        Get,
        Set
    };
    public interface IControllerFactory
    {
        ControllerWrapper CreateController(byte[] bytes);
        string Get { get; }
        string Post { get; }

    }
    public class ControllerFactory : IControllerFactory
    {
      
        ControllerType Type { get; set; }
        ControllerCommand Command { get; set; }
        ControllerBase Controller { get; set; }

        private ControllerWrapper InitializeController(string[] array)
        {
            ControllerWrapper wrapper = null;

            switch (this.Type)
            {
                case ControllerType.Main:
                    Controller = new MainController();
                    wrapper = new MainWrapper(Controller);
                    break;
                case ControllerType.Translate:
                    Controller = new TranslateController();
                    wrapper = new TranslateWrapper(Controller);
                    break;
                case ControllerType.Display:
                    Controller = new DisplayController();
                    wrapper = new DisplayWrapper(Controller);
                    break;
            }

            Controller?.Initialize(array);
            return wrapper;
        }
        void FromStringArray(string[] array)
        {
            Command = (ControllerCommand) Convert.ToByte(array[0]);
            Type = (ControllerType) Convert.ToByte(array[1]);
            InitializeController(array);
        }

        public ControllerWrapper CreateController(byte[] bytes)
        {
            var result = Encoding.ASCII.GetString(bytes);
            var array = result.Split('|');
            FromStringArray(array);

            return InitializeController(array);
        }

        public string Get =>  $"{(byte)ControllerCommand.Get}|{(byte)Type}\r";
        public string Post => $"{(byte)ControllerCommand.Set}|{Type}|{Controller}\r";

    }
}