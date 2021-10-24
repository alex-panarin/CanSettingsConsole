using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
