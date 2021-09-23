using System;
using System.Collections.Generic;
using System.Text;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.Models
{
    public class ControllerBase
    {
        public virtual string Name => nameof(ControllerBase);
        public byte Status { get; set; }
        public byte Sector { get; set; }
        public uint Code { get; set; }

        public virtual void Initialize(string[] array)
        {
            if(array.Length < 5) return;
            Status = Convert.ToByte(array[2]);
            Sector = Convert.ToByte(array[3]);
            Code = Convert.ToUInt32(array[4]);
        }

        public override string ToString()
        {
            return $"{Status}|{Sector}|{Code}";
        }
    }
}
