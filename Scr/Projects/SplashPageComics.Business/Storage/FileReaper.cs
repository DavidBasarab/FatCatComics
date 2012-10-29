using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace SplashPageComics.Business.Storage
{
    internal class FileReaper
    {
        public FileReaper(StorageFolder storageFolder)
        {
            Folder = storageFolder;
        }

        public StorageFolder Folder { get; set; }

        public async Task<IEnumerable<StorageFile>> ReapFiles()
        {
            var fileList = new List<StorageFile>();

            var folderList = await Folder.GetFoldersAsync();

            if (folderList.Count > 0)
            {
                foreach(var storageFolder in folderList)
                {
                    var currentFiles = await new FileReaper(storageFolder).ReapFiles();

                    fileList.AddRange(currentFiles);
                }
            }

            var files = await Folder.GetFilesAsync();

            fileList.AddRange(files);

            return fileList;
        }
    }
}