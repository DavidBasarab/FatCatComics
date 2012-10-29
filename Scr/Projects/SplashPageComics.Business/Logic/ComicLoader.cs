using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using NUnrar.Archive;
using NUnrar.Common;
using SplashPageComics.Business.Models;
using Windows.Storage;

namespace SplashPageComics.Business.Logic
{
    public class ComicLoader : ILoadComics
    {
        public IEnumerable<DisplayComic> InitialLoadOfComics(IEnumerable<StorageFile> files)
        {
            return InitialComicProcessor.Load(files);
        }
    }

    internal class InitialComicProcessor
    {
        public static IEnumerable<DisplayComic> Load(IEnumerable<StorageFile> files)
        {
            return new InitialComicProcessor(files).Process();
        }

        private static bool IsValidEntry(RarEntry entry)
        {
            return entry.Size == 0 || !entry.FilePath.ToLower().Contains(".jpg") || entry.IsDirectory;
        }

        private readonly List<DisplayComic> comics;

        private InitialComicProcessor(IEnumerable<StorageFile> files)
        {
            StorageFiles = files;
            comics = new List<DisplayComic>();
        }

        private IEnumerable<StorageFile> StorageFiles { get; set; }

        private bool IsValidEntry(ZipArchiveEntry entry)
        {
            return entry.Length == 0 || !entry.FullName.ToLower().Contains(".jpg");
        }

        private async Task LoadCBRComic(StorageFile storageFile)
        {
            try
            {
                using (var zipStream = await storageFile.OpenStreamForReadAsync())
                using (var memoryStream = new MemoryStream((int)zipStream.Length))
                {
                    await zipStream.CopyToAsync(memoryStream);

                    memoryStream.Position = 0;

                    var rarArchive = RarArchive.Open(memoryStream);

                    if (rarArchive == null) return;

                    var comic = new DisplayComic
                    {
                        File = storageFile,
                        FullPath = storageFile.Name,
                        Name = storageFile.DisplayName,
                        Pages = rarArchive.Entries.Count
                    };

                    foreach(var entry in rarArchive.Entries)
                    {
                        if (IsValidEntry(entry)) continue;

                        using (var entryStream = new MemoryStream((int)entry.Size))
                        {
                            entry.WriteTo(entryStream);
                            entryStream.Position = 0;

                            using (var binaryReader = new BinaryReader(entryStream))
                            {
                                var bytes = binaryReader.ReadBytes((int)entryStream.Length);

                                comic.CoverageImageBytes = bytes;

                                break;
                            }
                        }
                    }

                    comics.Add(comic);
                }
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        private async Task LoadCBZComic(StorageFile storageFile)
        {
            try
            {
                using (var zipStream = await storageFile.OpenStreamForReadAsync())
                using (var memoryStream = new MemoryStream((int)zipStream.Length))
                {
                    zipStream.Position = 0;

                    await zipStream.CopyToAsync(memoryStream);

                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                    {
                        foreach(var entry in archive.Entries)
                        {
                            if (IsValidEntry(entry)) continue;

                            var stream = entry.Open();

                            using (var binaryReader = new BinaryReader(stream))
                            {
                                var bytes = binaryReader.ReadBytes(2245999);

                                var comic = new DisplayComic
                                {
                                    File = storageFile,
                                    FullPath = storageFile.Name,
                                    Name = storageFile.DisplayName,
                                    Pages = archive.Entries.Count,
                                    CoverageImageBytes = bytes
                                };

                                comics.Add(comic);

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        private async void LoopThroughStorageFiles()
        {
            foreach(var storageFile in StorageFiles)
            {
                if (storageFile.FileType.ToLower().Contains("cbr")) await LoadCBRComic(storageFile);
                else if (storageFile.FileType.ToLower().Contains("cbz")) await LoadCBZComic(storageFile);
                else {}
            }

            var fileNames = StorageFiles.Select(i => i.DisplayName).ToList();
            var comicNames = comics.Select(i => i.Name).ToList();

            var missing = fileNames.Where(i => !comicNames.Contains(i)).ToList();
        }

        private IEnumerable<DisplayComic> Process()
        {
            LoopThroughStorageFiles();

            return comics;
        }
    }
}