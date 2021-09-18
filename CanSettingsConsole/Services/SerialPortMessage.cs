using System;
using System.Text;

namespace CanSettingsConsole.Services
{
    public enum ControllerType
    {
        None = 0,
        Main = 1,
        Translate = 2,
        Display = 3
    };

    public enum ControllerStatus : byte
    {
        Ready,
        Busy
    };

    public enum ControllerCommand : byte
    {
        None,
        Status,
        Get,
        Set
    };
   
    public class SerialPortMessage
    {
        public byte Command;
        public byte Status;
        public byte Type;
        public byte Sector;
        public uint Code;

        public override string ToString()
        {
            return $"{Command}|{Status}|{Type}|{Sector}|{Code}\r";
        }

        public SerialPortMessage( string res)
        {
            var array = res.Split('|');

            Command = Convert.ToByte(array[0]);
            Status = Convert.ToByte(array[1]);
            Type = Convert.ToByte(array[2]);
            Sector = Convert.ToByte(array[3]);
            Code = Convert.ToUInt32(array[4]);
        }
    }
}
