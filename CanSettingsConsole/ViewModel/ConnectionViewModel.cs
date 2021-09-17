using CanSettingsConsole.Core;
using System;
using System.IO.Ports;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.ViewModel
{
    public class ConnectionViewModel : ViewModelWrapper<SerialPort>, IConnectionViewModel
    {
        private readonly SerialPortService _serialPortService;

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
                _serialPortService.Open(Model);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
        public string Read()
        {
            throw new NotImplementedException();
        }
        public void Write(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
