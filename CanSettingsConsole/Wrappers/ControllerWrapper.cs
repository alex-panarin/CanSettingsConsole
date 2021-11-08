using CanSettingsConsole.Core;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CanSettingsConsole.UI;

namespace CanSettingsConsole.Wrappers
{
    public class ControllerWrapper : ViewModelWrapper<ControllerBase>, IDataErrorInfo
    {
        private readonly PropertyDescriptorCollection _propertyDescriptorCollection;

        public ControllerWrapper(ControllerBase model)
            : base(model)
        {
            _propertyDescriptorCollection = TypeDescriptor.GetProperties(this);
        }

        public string Name => Model.Name;
        public ControllerStatus Status => (ControllerStatus)Model.Status;

        [Required]
        [Range(1, 254, ErrorMessage = "Значение должно быть в диапазоне от 1 до 254")]
        public byte Sector
        {
            get => Model.Sector;
            set { Model.Sector = value; OnPropertyChanged();}
        }

        [Required]
        [Range(1, 32768, ErrorMessage = "Значение должно быть в диапазоне от 1 до 32768")]
        public uint Code
        {
            get => Model.Code;
            set { Model.Code = value; OnPropertyChanged();}
        }

        public virtual string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                var property = _propertyDescriptorCollection.Find(columnName, true);
                var validationContext = new ValidationContext(this)
                {
                    MemberName = columnName
                };

                var isValid = Validator.TryValidateProperty(property.GetValue(this), validationContext, validationResults);
                if (isValid)
                {
                    return null;
                }

                return validationResults.First().ErrorMessage;
            }
        }

        public virtual string Error
        {
            get
            {
                return _propertyDescriptorCollection
                    .OfType<PropertyDescriptor>()
                    .Select(x => this[x.Name])
                    .FirstOrDefault(x => x != null);
            }
        }
    }

}
