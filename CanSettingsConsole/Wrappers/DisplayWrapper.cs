using CanSettingsConsole.Core;
using CanSettingsConsole.Models;
using CanSettingsConsole.Services;
using CanSettingsConsole2.Services;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace CanSettingsConsole.Wrappers
{
    public class DisplayWrapper : ControllerWrapper
    {
        private readonly IControllerRepository _repository;
        private readonly IMessageContainer _messageContainer;

        public DisplayWrapper(ControllerBase model, 
            IControllerRepository repository,
            IMessageContainer  messageContainer) 
            : base(model)
        {
            SaveTemplateCommand = new WindowCommand(OnSaveTemplate);
            LoadTemplateCommand = new WindowCommand(OnLoadTemplate);
            SaveBatchCommand = new WindowCommand(OnSaveBatch);
            PlusGroupCommand  = new WindowCommand(OnPlusGroup);
            MinusGroupCommand = new WindowCommand(OnMinusGroup);
            _repository = repository;
            _messageContainer = messageContainer;
        }

        private void OnSaveBatch(object obj)
        {
            _repository.SaveTemplateData(((DisplayController)this.Model), false);
            _messageContainer.InvokeMessage(MessageContainer.SAVE_MESSAGE);
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
            _repository.SaveTemplateData(((DisplayController)this.Model), true);
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
        public ICommand SaveBatchCommand { get; }
        public ICommand PlusGroupCommand { get; }
        private void OnPlusGroup(object obj)
        {
            _messageContainer.InvokeMessage(MessageContainer.PLUS_MESSAGE);
        }
        public ICommand MinusGroupCommand { get; }
        private void OnMinusGroup(object obj)
        {
            _messageContainer.InvokeMessage(MessageContainer.MINUS_MESSAGE);
        }
    }
}
