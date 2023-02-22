using CanSettingsConsole.Core;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.Json;
using System.Windows.Input;

namespace CanSettingsConsole.Wrappers
{
    public class DisplayWrapper : ControllerWrapper
    {
        private readonly IControllerRepository _repository;
        
        public DisplayWrapper(ControllerBase model, 
            IControllerRepository repository) 
            : base(model)
        {
            SaveTemplateCommand = new WindowCommand(OnSaveTemplate);
            LoadTemplateCommand = new WindowCommand(OnLoadTemplate);
            _repository = repository;
        }

        private void OnLoadTemplate(object obj)
        {
            var display =  _repository.GetTemplateData();
            if (display == null)
                return;

            Mask = ConvertMask(display.Mask);
            Brightness = display.Brightness;
            Code = display.Code;
            ParentId = display.ParentId;
            Id = display.Id;
        }

        private void OnSaveTemplate(object obj)
        {
            _repository.SaveTemplateData(((DisplayController)this.Model));
        }

        [Required]
        [Range(1, 7, ErrorMessage = "Значение должно быть в диапазоне от 1 до 7")]
        public byte Brightness { get => ((DisplayController)Model).Brightness; set => SetValue(value); }

        public string Mask 
        {
            get
            {
                return ConvertMask(((DisplayController)Model).Mask);
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

        public ICommand SaveTemplateCommand { get; }

        public ICommand LoadTemplateCommand { get; }

        private string ConvertMask(byte mask)
        {
            var bits = new BitArray(new[] {mask});
            return string.Join('.', bits.Cast<bool>().Select(bit => bit ? 1 : 0));
        }
    }
}
