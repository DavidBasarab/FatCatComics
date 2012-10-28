using System.Threading.Tasks;

namespace SplashPageComics.Business.Data
{
    internal class SelectedFolderDataAccess : ISelectedFolderDataAccess
    {
        public SelectedFolderDataAccess(DataStore dataStore)
        {
            DataStore = dataStore;
        }

        private DataStore DataStore { get; set; }

        public async Task<int> NumberOfSelectedFolders()
        {
            return DataStore.SelectedFolders.Count;
        }
    }
}