using System.Runtime.CompilerServices;

namespace CanSettingsConsole.Core
{
    public class ViewModelWrapper<TModel> : ViewModelBase
    {
        public ViewModelWrapper(TModel model)
        {
            Model = model;
        }
        public TModel Model { get; }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            Model?.GetType().GetProperty(propertyName)?.SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)Model?.GetType().GetProperty(propertyName)?.GetValue(Model);
        }
    }
}
