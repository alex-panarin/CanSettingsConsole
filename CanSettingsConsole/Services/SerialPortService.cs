using CanSettingsConsole.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Ports;

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
        private const string getStatusString = "Status";
        private const int bytesToRead = 8;

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
                port.Open();

            port.WriteLine(getStatusString);
            
            ReadAsync(port, callback);
        }
        
        private void ReadAsync(SerialPort port, [NotNull]Action<ControllerBase> action)
        {
            byte[] buffer = new byte[bytesToRead];
            port.BaseStream.BeginRead(buffer, 0, bytesToRead,  (IAsyncResult ar) =>
            {
               try
               {
                   var actualLength = port.BaseStream.EndRead(ar);
                   port.BaseStream.Close();
                   port.Close();
                   
                   byte[] received = new byte[actualLength];
                   Buffer.BlockCopy(buffer, 0, received, 0, actualLength); 
                   
                   action(_controllerFactory.Create(received));

               }
               catch (IOException)
               {
                   throw;
               }
            }, null);


        }
    }
}
