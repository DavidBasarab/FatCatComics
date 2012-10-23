using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SplashPageComics.Business
{
    public abstract class SupportsPropertyChanges : INotifyPropertyChanged
    {
        /// <summary>
        ///     Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises <see cref="PropertyChanged" /> event/>
        /// </summary>
        /// <param name="propertyName"> Name of property to raise event for </param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Set property value
        /// </summary>
        /// <typeparam name="T"> Type of property </typeparam>
        /// <param name="value"> New value </param>
        /// <param name="backingField"> Backing field for property </param>
        /// <param name="propertyName"> Name of property, leave blank </param>
        protected void SetProperty<T>(T value, ref T backingField, [CallerMemberName] string propertyName = "")
        {
            if (backingField == null && value != null)
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }
            else if (backingField != null && value == null)
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }
            else if (backingField != null && value != null && !backingField.Equals(value))
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}