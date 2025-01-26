using CanSettingsConsole.Core;
using CanSettingsConsole.Services;
using CanSettingsConsole.Wrappers;
using CanSettingsConsole2.Services;
using System;
using System.IO.Ports;
using System.Text.Json;
using System.Windows;

namespace CanSettingsConsole.ViewModel
{
    public class ConnectionViewModel : ViewModelWrapper<SerialPort>, IConnectionViewModel
    {
        private readonly SerialPortService _serialPortService;
        private readonly MessageContainer _messageContainer;
        private ControllerWrapper _controller;
        //private bool _showMessage = true;
        
        public ConnectionViewModel(SerialPort model)
            : base(model)
        {
            _serialPortService = new SerialPortService();
            _messageContainer = MessageContainer.Instance;
            _messageContainer.RegisterMessage(MessageContainer.SAVE_MESSAGE, OnSaveBatch);
        }

        private void OnSaveBatch(object obj)
        {
            if (_controller == null) return;
            var oldValue = JsonSerializer.Serialize(_controller.Model);
            _serialPortService.Post(Model, _controller.Model);
            Open();
            var newValue = JsonSerializer.Serialize(_controller.Model);
            if(! oldValue.Equals(newValue, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Ошибка сохранения данных");
            }
            else
            {
                MessageBox.Show("Изменения успешно сохранены");
            }
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

        public WindowCommand SaveCommand => new(OnSave);

        private void OnSave(object obj)
        {
            if(_controller == null)
                return;

            _serialPortService.Post(Model, _controller.Model);
            MessageBox.Show("Изменения успешно сохранены");
        }

        public bool HasError
        {
            get => Controller == null;
            set => OnPropertyChanged();
        }
    }
}
