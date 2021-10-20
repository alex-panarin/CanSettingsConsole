using CanSettingsConsole.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Ports;
using System.Text;
using CanSettingsConsole.Wrappers;

namespace CanSettingsConsole.Services
{
    public interface ISerialPortService
    {
        void Close(SerialPort port);
        void Get(SerialPort port, Action<ControllerWrapper> callback);
        void Post(SerialPort port, ControllerBase controller);
    }
    public class SerialPortService : ISerialPortService
    {
        private readonly ControllerFactory _controllerFactory;
        public SerialPortService()
        {
            _controllerFactory = new ControllerFactory();
        }
        public void Close(SerialPort port)
        {
            if (port.IsOpen)
                port.Close();
        }

        public void Get(SerialPort port, Action<ControllerWrapper> callback)
        {
            if (!port.IsOpen)
            {
                port.Handshake = Handshake.None;
                port.ReadTimeout = 2000;
                port.Open();
            }

            var bytes = Encoding.ASCII.GetBytes(_controllerFactory.Get());
            port.Write(bytes, 0, bytes.Length);
            
            Read(port, callback);
        }

        public void Post(SerialPort port, ControllerBase controller)
        {
            var request = _controllerFactory.Post(controller);
            port?.Write(request);
        }

        internal void Read(SerialPort port, Action<ControllerWrapper> action)
        {
            var strToRead = port.ReadLine();

            action?.Invoke(_controllerFactory.CreateController(Encoding.ASCII
                .GetBytes(strToRead.TrimStart('\0'))));
        }
       
    }
}
