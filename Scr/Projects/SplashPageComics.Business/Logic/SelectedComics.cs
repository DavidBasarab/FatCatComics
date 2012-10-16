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

        public bool IsAtLeastOneFolderSelected()
        {
            return SelectedFolderDataAccess.NumberOfSelectedFolders() > 0;
        }
    }
}