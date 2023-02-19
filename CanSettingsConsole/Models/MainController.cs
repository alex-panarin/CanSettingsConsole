using System;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.Models
{
    public class MainController : ControllerBase
    {
        public override string Name => "Контроллер главный";
        public uint IpAddress { get; set; }
        public uint Gateway { get; set; }
        public uint Mask { get; set; }
        public uint Dns { get; set; }
        public bool UseDhcp { get; set; }
        public override void Initialize(string[] array)
        {
            base.Initialize(array);
            if (array.Length < 11) return;
            IpAddress = Convert.ToUInt32(array[6]);
            Dns = Convert.ToUInt32(array[7]);
            Gateway = Convert.ToUInt32(array[8]);
            Mask = Convert.ToUInt32(array[9]);
            UseDhcp = array[10] == "1";
        }
        public override string ToString()
        {
            return $"{Status}|{ParentId}|{Id}|{Code}|{IpAddress}|{Dns}|{Gateway}|{Mask}|{(UseDhcp ? 1 : 0)}";
        }
    }
}
