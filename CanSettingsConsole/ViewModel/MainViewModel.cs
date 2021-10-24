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
        private string _errorMessage;

        public MainViewModel()
        {
            ConnectCommand = new WindowCommand(ConnectToPort);
            CloseCommand = new WindowCommand(CloseConnection);
            ConnectionInfo = outOfConnection;
        }
        public ICommand ConnectCommand { get; }
        public ICommand CloseCommand { get; }

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

            try
            {
                ErrorMessage = string.Empty;
                ConnectionViewModel?.Open();
                ConnectionInfo = successConnection;
            }
            catch (Exception x)
            {
                ErrorMessage = x.Message;
            }
        }

        private void CloseConnection(object param)
        {
            ConnectionViewModel?.Close();
            ConnectionInfo = outOfConnection;
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
