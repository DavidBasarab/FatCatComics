using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;

namespace SplashPageComics.Business.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected BaseViewModel(MessengerService messengerService)
        {
            MessengerService = messengerService;

            RegisterForMessages();

            if (IsInDesignModeStatic) OnDesignTime();
        }

        public MessengerService MessengerService { get; set; }

        public new event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnDesignTime() {}

        public virtual void RegisterForMessages() {}

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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