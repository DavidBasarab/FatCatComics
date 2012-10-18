using System;

namespace SplashPageComics.Business.ViewModels
{
    public class Messenger : MessengerService
    {
        public void Send<T>(T value)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(value);
        }

        public void Register<T>(Action<T> handler)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register(this, handler);
        }

        public void Register<T>(object recipient, Action<T> handler)
        {
            if (GalaSoft.MvvmLight.Messaging.Messenger.Default != null) GalaSoft.MvvmLight.Messaging.Messenger.Default.Register(recipient, handler);
        }

        public void Unregister<T>(object recipient)
        {
            if (GalaSoft.MvvmLight.Messaging.Messenger.Default == null) return;

            var lockObj = new object();

            lock (lockObj)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<T>(recipient);
            }
        }
    }
}