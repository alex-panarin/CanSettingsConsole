using System;
using System.Collections.Generic;
using System.Text;

namespace CanSettingsConsole.Models
{
    public class TranslateController : ControllerBase
    {
        public override string Name => "Контроллер транслятор";
        public byte Level { get; set; }

    }
}
