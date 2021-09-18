using CanSettingsConsole.Core;
using System;
using System.IO.Ports;
using System.Windows.Input;

namespace CanSettingsConsole.ViewModel
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        const string outOfConnection = "Отсутсвует соединение";
        const string successConnection = "Соединение установлено";
        private string _connectionInfo;
        private IConnectionViewModel _connection;

        public MainViewModel()
        {
            ConnectCommand = new WindowCommand(ConnectToPort);
            ConnectionInfo = outOfConnection;
        }
        public ICommand ConnectCommand { get; }

        public string ConnectionInfo
        {
            get => _connectionInfo;
            set
            {
                _connectionInfo = value;
                OnPropertyChanged();
            }
        }

        public IConnectionViewModel ConnectionViewModel
        {
            get => _connection;
            set
            {
                _connection = value;
                OnPropertyChanged();
            }
        }

        public void Dispose()
        {
            ConnectionViewModel?.Close();
        }

        private void ConnectToPort(object param)
        {
            ConnectionViewModel?.Close();

            ConnectionInfo = outOfConnection;
            SerialPort port = (SerialPort)param;

            ConnectionViewModel = port != null 
                ? new ConnectionViewModel(port) 
                : null;

            if (ConnectionViewModel?.Open() == true)
                ConnectionInfo = successConnection;
        }
    }
}
