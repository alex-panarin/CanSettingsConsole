using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CanSettingsConsole.Models
{
    public class MainController : ControllerBase
    {
        public override string Name => "Контроллер главный";
        public bool UseDHCP { get; set; }
        public string IpAddress { get; set; }
        public string Gateway { get; set; }
        public string Mask { get; set; }
    }
}
