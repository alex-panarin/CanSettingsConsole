using System;

namespace CanSettingsConsole.Models
{
    public class DisplayController : ControllerBase
    {
        public byte Brightness { get; set; }
        public byte Mask { get; set; }
        public override string Name => "Контроллер индикации";
        public override string ToString()
        {
            return $"{Status}|{ParentId}|{Id}|{Code}|{Brightness}|{Mask}";
        }
        public override void Initialize(string[] array)
        {
            base.Initialize(array);
            if (array.Length < 6) return;

            Brightness = Convert.ToByte(array[6]);
            Mask = Convert.ToByte(array[7]);
        }
    }
}
