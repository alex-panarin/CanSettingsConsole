using System;
using System.IO.Ports;
using System.Text;

namespace CanSettingsConsole.Services
{
    public interface ISerialPortService
    {
        void Open(SerialPort port);
        void Close(SerialPort port);
        void Write(SerialPort port, byte[] bytes);
        string Read(SerialPort port);

    }
    public class SerialPortService : ISerialPortService
    {
        public void Open(SerialPort port)
        {
            port.Open();
        }

        public void Close(SerialPort port)
        {
            port.Close();
        }

        public void Write(SerialPort port, byte[] bytes)
        {
            port.WriteLine(Encoding.ASCII.GetString(bytes));
        }

        public string Read(SerialPort port)
        {
            return port.ReadLine();
        }
    }
}
