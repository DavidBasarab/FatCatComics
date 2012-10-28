using System.Threading.Tasks;
using SplashPageComics.Business.Data;

namespace SplashPageComics.Business.Logic
{
    internal class SelectedComics : SelectedComicsBusiness
    {
        private ISelectedFolderDataAccess SelectedFolderDataAccess { get; set; }

        public SelectedComics(ISelectedFolderDataAccess selectedFolderDataAccess)
        {
            SelectedFolderDataAccess = selectedFolderDataAccess;
        }

        public async Task<bool> IsAtLeastOneFolderSelected()
        {
            return await SelectedFolderDataAccess.NumberOfSelectedFolders() > 0;
        }
    }
}