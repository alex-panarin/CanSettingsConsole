using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CanSettingsConsole.Models;

namespace CanSettingsConsole.Wrappers
{
    public class DisplayWrapper : ControllerWrapper
    {
        public DisplayWrapper(ControllerBase model) : base(model)
        {
            
        }

        [Required]
        [Range(1, 7, ErrorMessage = "Значение должно быть в диапазоне от 1 до 7")]
        public byte Brightness { get => ((DisplayController)Model).Brightness; set => SetValue(value); }

        public string Mask 
        {
            get
            {
                var bits = new BitArray(new[] { ((DisplayController)Model).Mask });
                return string.Join('.', bits.Cast<bool>().Select(bit => bit ? 1 : 0));
                //string.Join('.', new BitArray(((DisplayController)Model).Mask).OfType<byte>());
            }
            set 
            {
                var bits = value.Split('.')
                    .Select(s => byte.Parse(s))
                    .ToArray();

                byte b = (byte)((bits[0] << 0)
                    | (bits[1] << 1)
                    | (bits[2] << 2)
                    | (bits[3] << 3)
                    | (bits[4] << 4)
                    | (bits[5] << 5)
                    | (bits[6] << 6)
                    | (bits[7] << 7));

                SetValue(b); 
            } 
        }
    }
}
