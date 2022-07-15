using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace UzbScales.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyPropertyChanged, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnProperyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnProperyChanged(PropertyName);
            return true;
        }
    }
}
