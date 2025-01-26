using CanSettingsConsole.Models;

namespace CanSettingsConsole.Wrappers
{
    public class TranslateWrapper : ControllerWrapper
    {
        public TranslateWrapper(ControllerBase model)
            : base(model){ } 
            public bool IsDuplicate
        {
            get => ((TranslateController)Model).IsDuplicate;
            set => SetValue(value, nameof(IsDuplicate));
        }
    }
}
