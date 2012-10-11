using System;
using Microsoft.Practices.Unity;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.Logic;
using SplashPageComics.Business.Storage;

namespace SplashPageComics.Business
{
    public class MainModule
    {
        public void Start()
        {
            var container = new UnityContainer();

            container.RegisterType<DataStore, Database>();
            container.RegisterType<FileAccess, StorageFileAccess>();
            container.RegisterType<SelectedComicsBusiness, SelectedComics>();

        }
    }

    public static class Unity
    {
        static Unity()
        {
            
        }
    }
}
