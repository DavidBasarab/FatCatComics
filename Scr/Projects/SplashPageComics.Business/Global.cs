using Microsoft.Practices.Unity;

namespace SplashPageComics.Business
{
    public static class Global
    {
        private static IUnityContainer Container { get; set; }

        public static T Create<T>()
        {
            return Container.Resolve<T>();
        }

        public static void SetContainer(IUnityContainer container)
        {
            Container = container;
        }
    }
}