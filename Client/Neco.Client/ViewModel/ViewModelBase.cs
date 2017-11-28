using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Neco.Client.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual ViewModelBase SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return this;
        }

        public T GetPropValue<T>(string propName)
        {
            if (propName != null) return (T)this.GetType()?.GetProperty(propName)?.GetValue(this, null);
            return default(T);
        }
    }
}
