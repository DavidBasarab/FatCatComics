using Microsoft.Practices.Unity;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.Logic;
using SplashPageComics.Business.Storage;

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

                Global.SetContainer(container);
            }
            catch {}
        }
    }
}