using Windows.Storage;

namespace Spikes.Models
{
    public class FolderModel : BaseDisplayModel
    {
        public FolderModel(string displayName)
            : base(displayName) {}

        public FolderModel(StorageFolder storageFolder)
            : base(storageFolder.Name)
        {
            StorageFile = storageFolder;
        }

        public StorageFolder StorageFile { get; set; }
    }
}