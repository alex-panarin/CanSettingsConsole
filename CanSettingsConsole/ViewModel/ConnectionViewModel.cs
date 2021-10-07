using CanSettingsConsole.Core;
using System;
using System.Diagnostics;
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
            Controller = null;
        }
        public bool Open()
        {
            try
            {
                _serialPortService.Get(Model, (ControllerWrapper c) => { Controller = c; });

            }
            catch(Exception x)
            {
                Close();
                Debug.WriteLine(x.Message);
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

        public WindowCommand SaveCommand => new WindowCommand(OnSave);

        private void OnSave(object obj)
        {
            //var controller = (obj as ControllerWrapper)?.Model;
            _serialPortService.Post(Model, _controller.Model);
        }
    }
}
