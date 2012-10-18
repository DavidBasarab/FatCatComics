using System;

namespace SplashPageComics.Business.ViewModels
{
    public interface MessengerService
    {
        void Register<T>(Action<T> handler);

        void Register<T>(object recipient, Action<T> handler);

        void Send<T>(T value);

        void Unregister<T>(object recipient);
    }
}