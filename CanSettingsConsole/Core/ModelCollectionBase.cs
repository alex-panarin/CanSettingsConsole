using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CanSettingsConsole.Core
{
    public class ModelCollectionBase<TModel> : ObservableCollection<TModel>
    {
        class Locker : IDisposable
        {
            private readonly ModelCollectionBase<TModel> _collection;
            public Locker(ModelCollectionBase<TModel> collection)
            {
                _collection = collection;
                _collection.LockRaiseEvent = true;
            }
            public void Dispose()
            {
                _collection.LockRaiseEvent = false;
                _collection.RaiseCollectionChanged();
            }
        }

        private bool LockRaiseEvent { get; set; }
        private void RaiseCollectionChanged()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public ModelCollectionBase() : base()
        {

        }
        public ModelCollectionBase(IEnumerable<TModel> collection) :
            base(collection)
        {
        }
        public void AddRange(IEnumerable<TModel> collection)
        {
            if (collection == null) return;

            using (IEnumerator<TModel> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    this.Add(enumerator.Current);
                }
            }
        }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (LockRaiseEvent) return;
            base.OnCollectionChanged(e);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (LockRaiseEvent) return;
            base.OnPropertyChanged(e);
        }
        public IDisposable LockChangedEvent()
        {
            return new Locker(this);
        }

        protected virtual void SetValue<TValue>(TValue value, TModel model, [CallerMemberName] string propertyName = null)
        {
            typeof(TModel).GetProperty(propertyName).SetValue(model, value);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected TValue GetValue<TValue>(TModel model, [CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(TModel).GetProperty(propertyName).GetValue(model);
        }
    }
}
