using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CanSettingsConsole.Services
{
    public enum ControllerType
    {
        None = 0,
        Main = 1,
        Translate = 2,
        Display = 3
    }

    public enum ControllerStatus : byte
    {
        Ready,
        Busy 
    }

    [StructLayout(LayoutKind.Explicit, Size = 64, CharSet = CharSet.Ansi)]
    public struct SerialPortPayload
    {
        [FieldOffset(0)] public ulong Status;
        [FieldOffset(2)] public ulong Type;
        [FieldOffset(4)] public ulong Sector;
        [FieldOffset(8)] public ulong Code;

        public static explicit operator SerialPortPayload(ulong v)
        {
            return new SerialPortPayload()
            {
                Status = v & 0xC0, 
                Type = v << 2 & 0xC0,
                Sector = v << 4 & 0xF0,
                Code = v << 8
            };
        }

        public static explicit operator ulong(SerialPortPayload v) => ((ulong)v.Code << 6 | v.Status);
        public override string ToString() => $"{(ControllerStatus)Status},{(ControllerType)Type},{Sector},{Code}";

        public override bool Equals(object obj) => 
            obj is SerialPortPayload pl && pl.Code == Code && pl.Status == Status;

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

}
