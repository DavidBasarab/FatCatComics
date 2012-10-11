using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using NUnrar.Archive;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Spikes.Models
{
    public class FileModel : BaseDisplayModel
    {
        private WriteableBitmap coverImage;
        private bool showCover;

        public FileModel(string displayName)
            : base(displayName) {}

        public FileModel(StorageFile storageFile)
            : base(storageFile.Path)
        {
            StorageFile = storageFile;
        }

        public StorageFile StorageFile { get; set; }

        public bool ShowCover
        {
            get { return showCover; }
            set
            {
                showCover = value;
                RaisePropertyChanged("ShowCover");
            }
        }

        public WriteableBitmap CoverImage
        {
            get { return coverImage; }
            set
            {
                coverImage = value;
                RaisePropertyChanged("CoverImage");
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public async Task FindCover(Action<IRandomAccessStream> loadFileExternally = null)
        {
            try
            {
                if (StorageFile.FileType.Contains("cbz"))
                {
                    FindCbrCover(loadFileExternally);

                    return;
                }

                using (var zipStream = await StorageFile.OpenStreamForReadAsync())
                using (var zipMemoryStream = new MemoryStream((int)zipStream.Length))
                {
                    await zipStream.CopyToAsync(zipMemoryStream);

                    zipMemoryStream.Position = 0;

                    var rarArchive = RarArchive.Open(zipMemoryStream);

                    if (rarArchive == null) return;

                    foreach(var entry in rarArchive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        using (var tempMemStream = new MemoryStream((int)entry.Size))
                        {
                            if (entry.Size == 0) continue;

                            if (!entry.FilePath.ToLower().Contains(".jpg")) continue;

                            entry.WriteTo(tempMemStream);

                            tempMemStream.Position = 0;

                            var randomAccessStream = await MicrosoftStreamExtensions.ConvertToRandomAccessStream(tempMemStream);

                            LoadCover(randomAccessStream);

                            if (loadFileExternally != null) loadFileExternally(randomAccessStream.CloneStream());

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Put in error handling
            }
        }

        private async Task FindCbrCover(Action<IRandomAccessStream> loadFileExternally = null)
        {
            try
            {
                using (var zipStream = await StorageFile.OpenStreamForReadAsync())
                using (var zipMemoryStream = new MemoryStream((int)zipStream.Length))
                {
                    zipMemoryStream.Position = 0;

                    await zipStream.CopyToAsync(zipMemoryStream);

                    using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Read))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            if (entry.Length == 0) continue;

                            if (!entry.FullName.ToLower().Contains(".jpg")) continue;

                            var tempPath = ApplicationData.Current.TemporaryFolder;

                            var tempFile = await tempPath.CreateFileAsync(string.Format("{0}.fcr", Guid.NewGuid().ToString()), CreationCollisionOption.ReplaceExisting);

                            var stream = entry.Open();

                            var ras = await tempFile.OpenAsync(FileAccessMode.ReadWrite);

                            await stream.CopyToAsync(ras.AsStreamForWrite());

                            LoadCover(ras.CloneStream());

                            if (loadFileExternally != null) loadFileExternally(ras.CloneStream());

                            ras.Dispose();

                            tempFile.DeleteAsync(StorageDeleteOption.PermanentDelete);

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LoadCover(IRandomAccessStream stream)
        {
            var image = new WriteableBitmap(1, 1);

            image.SetSource(stream);

            CoverImage = image;

            ShowCover = true;
        }
    }
}