using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CanSettingsConsole.Core;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;

namespace CanSettingsConsole.Wrappers
{
    public class ControllerWrapper : ViewModelWrapper<ControllerBase>
    {
        public ControllerWrapper(ControllerBase model)
            : base(model)
        {
        }

        public string Name => Model.Name;
        public ControllerStatus Status
        { 
            get => (ControllerStatus)Model.Status;
            set { Model.Status = (byte)value; OnPropertyChanged(); }
        }

        public byte Sector
        {
            get => Model.Sector;
            set { Model.Sector = value; OnPropertyChanged(); }
        }

        public uint Code
        {
            get => Model.Code;
            set { Model.Code = value; OnPropertyChanged();}
        }
    }

}
