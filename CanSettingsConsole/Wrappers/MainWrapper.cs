using System;
using System.Collections.Generic;
using System.Text;
using CanSettingsConsole.Models;

namespace CanSettingsConsole.Wrappers
{
    public class MainWrapper : ControllerWrapper
    {
        public MainWrapper(ControllerBase model) : base(model)
        {
        }

        public string IpAddress { get => ((MainController)Model).IpAddress; set => SetValue(value); }
        public string Gateway { get => ((MainController)Model).Gateway; set => SetValue(value); }
        public string Mask { get => ((MainController)Model).Mask; set => SetValue(value); }
        public bool UseDHCP { get => ((MainController)Model).UseDHCP; set => SetValue(value); }
    }
}
