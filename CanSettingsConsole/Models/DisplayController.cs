using System;
using System.Collections.Generic;
using System.Text;

namespace CanSettingsConsole.Models
{
    public class DisplayController : ControllerBase
    {
        public byte Brightness { get; set; }
        public override string Name => "Контроллер индикации";
        public override string ToString()
        {
            return $"{Status}|{Sector}|{Code}|{Brightness}";
        }

        public override void Initialize(string[] array)
        {
            base.Initialize(array);
            if (array.Length < 6) return;

            Brightness = Convert.ToByte(array[5]);

        }
    }
}
