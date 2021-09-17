using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CanSettingsConsole.Core
{
    public class ViewModelWrapper<TModel> : ViewModelBase
    {
        public ViewModelWrapper(TModel model)
        {
            Model = model;
        }
        public TModel Model { get; private set; }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(TModel).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(TModel).GetProperty(propertyName).GetValue(Model);
        }
    }
}
