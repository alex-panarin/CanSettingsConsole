using CanSettingsConsole.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace CanSettingsConsole.Services
{
    public interface ISerialPortService
    {
        void Close(SerialPort port);
        void Connect(SerialPort port, Action<ControllerBase> callback);
    }
    public class SerialPortService : ISerialPortService
    {
        private readonly ControllerFactory _controllerFactory;
        private const int bytesToRead = 20;

        public SerialPortService()
        {
            _controllerFactory = new ControllerFactory();
        }
        public void Close(SerialPort port)
        {
            if (port.IsOpen)
                port.Close();
        }

        public void Connect(SerialPort port, Action<ControllerBase> callback)
        {
            if (!port.IsOpen)
            {
                port.Handshake = Handshake.None;
                port.ReadTimeout = 2000;
                port.Open();
            }

            var bytes = Encoding.ASCII
                .GetBytes(new SerialPortMessage {Command = (byte) ControllerCommand.Status}
                    .ToString()
                    .ToCharArray());

            port.Write(bytes, 0, bytes.Length);
            
            Read(port, callback);
        }

        private void Read(SerialPort port, Action<ControllerBase> action)
        {
            var strToRead = port.ReadLine();

            action?.Invoke(_controllerFactory.Create(strToRead.TrimStart('\0')));
        }
        private async void ReadAsync(SerialPort port, Action<ControllerBase> action)
        {
            byte[] buffer = new byte[bytesToRead];

            var actualRead = await port.BaseStream.ReadAsync(buffer, 0, bytesToRead);
            if(actualRead == 0) return;

            action?.Invoke( _controllerFactory.Create(buffer));
        }
    }
}
