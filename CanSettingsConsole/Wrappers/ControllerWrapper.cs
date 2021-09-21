using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ControllerStatus Status => (ControllerStatus)Model.Status;

        [Required]
        [Range(typeof(byte), "1" , "254", ErrorMessage = "Значение должно содержать только цифры и быть больше 0 и меньше 255")]
        public byte Sector
        {
            get => Model.Sector;
            set { Model.Sector = value; OnPropertyChanged(); }
        }

        [Required]
        [Range(typeof(uint), "1", "99999999999", ErrorMessage = "Значение должно содержать только цифры и быть больше 0 и меньше 99 999 999 999")]
        public uint Code
        {
            get => Model.Code;
            set { Model.Code = value; OnPropertyChanged();}
        }
    }

}
