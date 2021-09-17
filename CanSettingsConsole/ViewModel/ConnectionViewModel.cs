using CanSettingsConsole.Core;
using System;
using System.IO.Ports;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.ViewModel
{
    public class ConnectionViewModel : ViewModelWrapper<SerialPort>, IConnectionViewModel
    {
        private readonly SerialPortService _serialPortService;
        private ControllerBase _controller;

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
                Controller = _serialPortService.GetController(Model);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
      
        public ControllerBase Controller
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
