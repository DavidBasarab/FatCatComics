using System;
using System.Threading.Tasks;
using SplashPageComics.Business.DataTypes;
using SplashPageComics.Business.Models;
using Windows.Storage.Pickers;

namespace SplashPageComics.Business.Storage
{
    internal class StorageFileAccess : FileAccess
    {
        public async Task<UserSelectedFolder> PickFolder()
        {
            var picker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.ComputerFolder
            };

            picker.FileTypeFilter.Add(".cbr");
            picker.FileTypeFilter.Add(".cbz");

            var folder = await picker.PickSingleFolderAsync();

            return new UserSelectedFolder
            {
                FolderLocation = folder.Name,
                DateAdded = DateTime.UtcNow,
                StorageFolder = folder
            };
        }

        public async Task<string> PickFile()
        {
            throw new NotImplementedException();
        }
    }
}