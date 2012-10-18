using Microsoft.Practices.Unity;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.Logic;
using SplashPageComics.Business.Storage;
using SplashPageComics.Business.ViewModels;

namespace SplashPageComics.Business
{
    public static class MainModule
    {
        public static void Start()
        {
            try
            {
                var container = new UnityContainer();

                container.RegisterType<ISelectedFolderDataAccess, SelectedFolderDataAccess>();
                container.RegisterType<FileAccess, StorageFileAccess>();
                container.RegisterType<SelectedComicsBusiness, SelectedComics>();
                container.RegisterInstance(SplashDatabase.Instance, new ExternallyControlledLifetimeManager());
                container.RegisterType<MessengerService, Messenger>();

                Global.SetContainer(container);

                ViewLocator.MainViewModel.Start();
            }
            catch {}
        }
    }
}