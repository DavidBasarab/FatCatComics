using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SplashPageComics.Business.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace SplashPageComics.Business.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool isProcessing;

        protected BaseViewModel(MessengerService messengerService, ThreadManagement threadManagement)
        {
            MessengerService = messengerService;
            ThreadManagement = threadManagement;

            RegisterForMessages();

            if (IsInDesignModeStatic) OnDesignTime();
            else ThreadManagement.ExecuteInSeparateThread(RunStartUp);
        }

        protected NavigationService NavigationService
        {
            get { return NavigationService.Service; }
        }

        protected ThreadManagement ThreadManagement { get; set; }

        public MessengerService MessengerService { get; set; }

        public bool IsProcessing
        {
            get { return isProcessing; }
            set { SetProperty(value, ref isProcessing); }
        }

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
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

            if (backingField == null && value != null)
            {
                backingField = value;

                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => RaisePropertyChanged(propertyName));
            }
            else if (backingField != null && value == null)
            {
                backingField = value;

                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => RaisePropertyChanged(propertyName));
            }
            else if (backingField != null && value != null && !backingField.Equals(value))
            {
                backingField = value;

                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => RaisePropertyChanged(propertyName));
            }
        }
    }
}