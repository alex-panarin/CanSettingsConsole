using System;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.Models
{
    public class MainController : ControllerBase
    {
        public override string Name => "Контроллер главный";
        public uint Gateway { get; set; }
        public uint Mask { get; set; }
        public uint Dns { get; set; }
        public bool UseDhcp { get; set; }
        public override void Initialize(string[] array)
        {
            base.Initialize(array);
            if (array.Length < 9) return;

            Dns = Convert.ToUInt32(array[5]);
            Gateway = Convert.ToUInt32(array[6]);
            Mask = Convert.ToUInt32(array[7]);
            UseDhcp = array[8] == "1";
        }
        public override string ToString()
        {
            return $"{Status}|{Sector}|{Code}|{Dns}|{Gateway}|{Mask}|{UseDhcp}";
        }
    }
}
