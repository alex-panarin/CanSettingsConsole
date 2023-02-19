using System;

namespace CanSettingsConsole.Models
{
    public class ControllerBase
    {
        public virtual string Name => nameof(ControllerBase);
        public byte Status { get; set; }
        public byte ParentId { get; set; }
        public byte Id { get; set; }
        public uint Code { get; set; }
        public byte Type { get; private set; }
        public virtual void Initialize(string[] array)
        {
            Type = Convert.ToByte(array[1]);
            if (array.Length < 5) return;
            Status = Convert.ToByte(array[2]);
            ParentId = Convert.ToByte(array[3]);
            Id = Convert.ToByte(array[4]);
            Code = Convert.ToUInt32(array[5]);
        }

        public override string ToString()
        {
            return $"{Status}|{ParentId}|{Id}|{Code}";
        }
    }
}
