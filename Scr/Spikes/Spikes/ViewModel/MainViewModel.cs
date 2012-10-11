using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spikes.Models;
using Windows.Storage.Streams;

namespace Spikes.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string debugMessages;
        private FileModel fileSelected;
        private ObservableCollection<FileModel> foundFiles;
        private ObservableCollection<FolderModel> foundFolders;
        private FolderModel selectedFolder;
        private DatabaseTestStuff testStuff;

        public MainViewModel()
        {
            RunCommand = new RelayCommand(OnRunClick);
            FileSelectedCommand = new RelayCommand(OnFileSelected);
            FolderSelectedCommand = new RelayCommand(OnFolderSelected);

            if (IsInDesignModeStatic) SetDesignInformation();
        }

        public ICommand RunCommand { get; set; }
        public ICommand FileSelectedCommand { get; set; }
        public ICommand FolderSelectedCommand { get; set; }

        public FileModel FileSelected
        {
            get { return fileSelected; }
            set
            {
                fileSelected = value;
                RaisePropertyChanged("FileSelected");
            }
        }

        public FolderModel SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                selectedFolder = value;
                RaisePropertyChanged("SelectedFolder");
            }
        }

        public ObservableCollection<FolderModel> FoundFolders
        {
            get { return foundFolders; }
            set
            {
                foundFolders = value;
                RaisePropertyChanged("FoundFolders");
            }
        }

        public ObservableCollection<FileModel> FoundFiles
        {
            get { return foundFiles; }
            set
            {
                foundFiles = value;
                RaisePropertyChanged("FoundFiles");
            }
        }

        public string DebugMessages
        {
            get { return debugMessages; }
            set
            {
                debugMessages = value;
                RaisePropertyChanged("DebugMessages");
            }
        }

        public bool RunAtLeastOnce { get; set; }

        public event Action<IRandomAccessStream> LoadImageFile;

        private async void OnFileSelected()
        {
            DebugMessages = string.Format("Selected File |||||  {0}", FileSelected);

            FileSelected.FindCover(LoadImageFile);
        }

        private async void OnFolderSelected()
        {
            DebugMessages = string.Format("Folder selected |||||| {0}", SelectedFolder.DisplayName);

            try
            {
                var folderList = await new FileReaper(SelectedFolder.StorageFile).ReapFiles();

                var fileList = new List<FileModel>();

                foreach(var storageFile in folderList)
                {
                    var fileModel = new FileModel(storageFile);

                    await fileModel.FindCover();

                    fileList.Add(fileModel);
                }

                FoundFiles = new ObservableCollection<FileModel>(fileList);
            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex);
            }
        }

        private async void OnRunClick()
        {
            try
            {
                if (!RunAtLeastOnce)
                {
                    testStuff = new DatabaseTestStuff();

                    await testStuff.RunTest();
                }

                RunAtLeastOnce = true;

                //await testStuff.CreateDummyComic();
                //await testStuff.DeleteFirstComic();

                var comic = testStuff.FindComicByTitle("634854153039764517");

                WriteMessage(string.Format("Comic File Name: {0}", comic.FileLocation));


                RemoveComics();
            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex);
            }

            //try
            //{
            //    var picker = new FolderPicker
            //    {
            //        ViewMode = PickerViewMode.List,
            //        SuggestedStartLocation = PickerLocationId.ComputerFolder
            //    };

            //    picker.FileTypeFilter.Add(".cbr");
            //    picker.FileTypeFilter.Add(".cbz");
            //    picker.FileTypeFilter.Add(".txt");
            //    picker.FileTypeFilter.Add(".rar");

            //    var folder = await picker.PickSingleFolderAsync();

            //    if (folder == null) return;

            //    var folders = await folder.GetFoldersAsync();

            //    if (folders.Count == 0)
            //    {
            //        SelectedFolder = new FolderModel(folder);

            //        OnFolderSelected();

            //        return;
            //    }

            //    var listDisplayFolders = new List<FolderModel>();

            //    foreach(var storageFolder in folders)
            //    {
            //        var folderModel = new FolderModel(storageFolder);

            //        listDisplayFolders.Add(folderModel);
            //    }

            //    FoundFolders = new ObservableCollection<FolderModel>(listDisplayFolders);
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorMessage(ex);
            //}
        }

        private void RemoveComics()
        {
            var comics = testStuff.SimpleComics();

            WriteMessage(string.Format("Number of Comics {0}", comics.Count));

            foreach(var spikeDBObject in comics) WriteMessage(string.Format("Title: {0}", spikeDBObject.Title));
        }

        private void SetDesignInformation()
        {
            var testFolders = new List<FolderModel>
            {
                new FolderModel("Folder 1"),
                new FolderModel("Gary"),
                new FolderModel("Sally"),
                new FolderModel("Carson"),
                new FolderModel("Chris")
            };

            FoundFolders = new ObservableCollection<FolderModel>(testFolders);

            var testFile = new List<FileModel>
            {
                new FileModel("File 1"),
                new FileModel("Andy"),
                new FileModel("AJ"),
                new FileModel("Hitman")
            };

            FoundFiles = new ObservableCollection<FileModel>(testFile);
        }

        private void WriteErrorMessage(Exception ex)
        {
            var errorMessage = new StringBuilder();

            errorMessage.AppendLine(string.Format("Error:      {0}", ex.Message));
            errorMessage.AppendLine(string.Format("Stacktrace: {0}", ex.StackTrace));

            WriteMessage(errorMessage.ToString());
        }

        private void WriteMessage(string message)
        {
            DebugMessages += string.Format("{0}{1}", Environment.NewLine, message);
            RaisePropertyChanged("DebugMessages");
        }
    }
}