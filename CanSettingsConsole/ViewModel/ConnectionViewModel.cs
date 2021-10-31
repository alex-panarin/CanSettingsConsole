using CanSettingsConsole.Core;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;
using System.Windows.Documents;
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
                throw new Exception(x.Message);
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
                HasError = _controller == null;
            }
        }

        public WindowCommand SaveCommand => new WindowCommand(OnSave);

        private void OnSave(object obj)
        {
            if(_controller == null)
                return;

            _serialPortService.Post(Model, _controller.Model);
            MessageBox.Show("Изменения успешно сохранены");
            //_serialPortService.Read(Model, x =>
            //{
            //    if(x == null) return;

            //    Debug.WriteLine(x);
            //});
        }

        public bool HasError
        {
            get => Controller == null;
            set => OnPropertyChanged();
        }
    }
}
