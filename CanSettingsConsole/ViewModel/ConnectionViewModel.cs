using CanSettingsConsole.Core;
using System;
using System.IO.Ports;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;
using CanSettingsConsole.Wrappers;

namespace CanSettingsConsole.ViewModel
{
    public class ConnectionViewModel : ViewModelWrapper<SerialPort>, IConnectionViewModel
    {
        private readonly SerialPortService _serialPortService;
        private ControllerWrapper _controller;

        public ConnectionViewModel(SerialPort model)
            : base(model)
        {
            _serialPortService = new SerialPortService();
        }
        public void Close()
        {
            _serialPortService.Close(Model);
        }
        public bool Open()
        {
            try
            {
                _serialPortService.Connect(Model, (ControllerBase c) =>
                {
                    Controller = new ControllerWrapper(c);
                });
                
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
      
        public ControllerWrapper Controller
        {
            get => _controller;
            set
            {
                _controller = value;
                OnPropertyChanged();
            }
        }

    }
}
