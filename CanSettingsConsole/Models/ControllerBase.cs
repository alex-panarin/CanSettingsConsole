using System;
using System.Collections.Generic;
using System.Text;

namespace CanSettingsConsole.Models
{
    public class ControllerBase
    {
        public virtual string Name => nameof(ControllerBase);
        public byte Status { get; set; }
        public byte Sector { get; set; }
        public uint Code { get; set; }

    }
}
