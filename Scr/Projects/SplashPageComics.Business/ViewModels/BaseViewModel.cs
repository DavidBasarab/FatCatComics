using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using SplashPageComics.Business.Threading;

namespace SplashPageComics.Business.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected BaseViewModel(MessengerService messengerService, ThreadManagement threadManagement)
        {
            MessengerService = messengerService;
            ThreadManagement = threadManagement;

            RegisterForMessages();

            if (IsInDesignModeStatic) OnDesignTime();
            else ThreadManagement.ExecuteInSeparateThread(RunStartUp);
        }

        protected ThreadManagement ThreadManagement { get; set; }

        public MessengerService MessengerService { get; set; }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnDesignTime() {}

        protected virtual void RegisterForMessages() {}

        protected virtual void RunStartUp() {}

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