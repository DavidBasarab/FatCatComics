using System;
using Windows.UI.Xaml.Controls;

namespace SplashPageComics.Business
{
    public class NavigationService
    {
        public static void Create(Frame frame)
        {
            Service = new NavigationService(frame);
        }

        public static NavigationService Service { get; set; }

        private readonly Frame frame;

        private NavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void GoBack()
        {
            frame.GoBack();
        }

        public void GoForward()
        {
            frame.GoForward();
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            return frame.Navigate(source, parameter);
        }
    }
}