using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ArcTouch.Movies.Helpers
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore,
                                      T value,
                                      [CallerMemberName]string propertyName = "",
                                      Action onChanged = null)
        {
            if (null != value &&
                null != backingStore &&
                backingStore.Equals(value))
            {
                return false;
            }

            backingStore = value;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
