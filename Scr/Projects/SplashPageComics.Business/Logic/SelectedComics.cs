using System.Threading.Tasks;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.Models;
using SplashPageComics.Business.Storage;

namespace SplashPageComics.Business.Logic
{
    internal class SelectedComics : SelectedComicsBusiness
    {
        public SelectedComics(ISelectedFolderDataAccess selectedFolderDataAccess, ILoadComics comicLoader)
        {
            SelectedFolderDataAccess = selectedFolderDataAccess;
            ComicLoader = comicLoader;
        }

        private ISelectedFolderDataAccess SelectedFolderDataAccess { get; set; }
        private ILoadComics ComicLoader { get; set; }

        public async Task<bool> IsAtLeastOneFolderSelected()
        {
            return await SelectedFolderDataAccess.NumberOfSelectedFolders() > 0;
        }

        public async Task ReapComics(UserSelectedFolder selectedFolder)
        {
            var reaper = new FileReaper(selectedFolder.StorageFolder);

            var files = await reaper.ReapFiles();

            var comics = ComicLoader.InitialLoadOfComics(files);
        }
    }
}