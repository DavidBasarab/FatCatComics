using System;

namespace SplashPageComics.Business.Data
{
    internal class SelectedFolderDataAccess : ISelectedFolderDataAccess
    {
        private DataStore DataStore { get; set; }

        public SelectedFolderDataAccess(DataStore dataStore)
        {
            DataStore = dataStore;
        }

        public int NumberOfSelectedFolders()
        {
            return DataStore.SelectedFolders.Count;
        }
    }
}