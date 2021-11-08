using CanSettingsConsole.Models;
using System;
using System.Net;

namespace CanSettingsConsole.Wrappers
{
    public class MainWrapper : ControllerWrapper
    {
        public MainWrapper(ControllerBase model) : base(model)
        {
        }

        public string IpAddress
        {
            get => new IPAddress(Model.Code).ToString(); 
            set => SetValue(BitConverter.ToUInt32(IPAddress.Parse(value).GetAddressBytes()), "Code");
        }

        public string Dns
        {
            get => new IPAddress(((MainController)Model).Dns).ToString(); 
            set => SetValue(BitConverter.ToUInt32(IPAddress.Parse(value).GetAddressBytes()));
        }

        public string Gateway
        {
            get => new IPAddress(((MainController)Model).Gateway).ToString(); 
            set => SetValue(BitConverter.ToUInt32(IPAddress.Parse(value).GetAddressBytes()));
        }

        public string Mask
        {
            get => new IPAddress(((MainController)Model).Mask).ToString(); 
            set => SetValue(BitConverter.ToUInt32(IPAddress.Parse(value).GetAddressBytes()));
        }
        public bool UseDhcp{ get => ((MainController)Model).UseDhcp; set => SetValue(value); }
        
    }
}
