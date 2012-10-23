using System;
using System.Threading.Tasks;
using SplashPageComics.Business.DataTypes;
using Windows.Storage.Pickers;

namespace SplashPageComics.Business.Storage
{
    internal class StorageFileAccess : FileAccess
    {
        public async Task<SelectedFolder> PickFolder()
        {
            var picker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.ComputerFolder
            };

            picker.FileTypeFilter.Add(".cbr");
            picker.FileTypeFilter.Add(".cbz");

            var folder = await picker.PickSingleFolderAsync();

            return new SelectedFolder
            {
                FolderLocation = folder.Name,
                DateAdded = DateTime.UtcNow
            };
        }

        public async Task<string> PickFile()
        {
            throw new NotImplementedException();
        }
    }
}