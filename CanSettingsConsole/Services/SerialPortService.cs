using System;
using System.IO.Ports;
using System.Text;
using CanSettingsConsole.Models;

namespace CanSettingsConsole.Services
{
    public interface ISerialPortService
    {
        void Close(SerialPort port);
        ControllerBase GetController(SerialPort port);
    }
    public class SerialPortService : ISerialPortService
    {
        private const string getStatusString = "Status";

        public void Close(SerialPort port)
        {
            if (port.IsOpen)
                port.DataReceived -= Port_DataReceived;

            port.Close();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }

        public ControllerBase GetController(SerialPort port)
        {
            if (!port.IsOpen)
            {
                port.Open();
                port.DataReceived += Port_DataReceived;
            }

            port.WriteLine(getStatusString);

            return new ControllerBase();
        }

    }
}
