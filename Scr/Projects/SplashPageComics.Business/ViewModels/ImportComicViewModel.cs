using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SplashPageComics.Business.Logic;
using SplashPageComics.Business.Models;
using SplashPageComics.Business.Storage;
using SplashPageComics.Business.Threading;

namespace SplashPageComics.Business.ViewModels
{
    public class ImportComicViewModel : BaseViewModel
    {
        private string fileMessage;
        private string folderMessage;

        public ImportComicViewModel()
            : this(Global.Create<MessengerService>(), Global.Create<ThreadManagement>(), Global.Create<SelectedComicsBusiness>(), Global.Create<FileAccess>()) {}

        public ImportComicViewModel(MessengerService messengerService, ThreadManagement threadManagement, SelectedComicsBusiness selectedComicsBusiness, FileAccess fileAccess)
            : base(messengerService, threadManagement)
        {
            SelectedComicsBusiness = selectedComicsBusiness;
            FileAccess = fileAccess;
        }

        private FileAccess FileAccess { get; set; }

        public string FolderMessage
        {
            get { return folderMessage; }
            set { SetProperty(value, ref folderMessage); }
        }

        public string FileMessage
        {
            get { return fileMessage; }
            set { SetProperty(value, ref fileMessage); }
        }

        public ICommand SelectFolderCommand { get; set; }
        public ICommand SelectFileCommand { get; set; }

        private SelectedComicsBusiness SelectedComicsBusiness { get; set; }

        protected override void RegisterForMessages()
        {
            SelectFolderCommand = new RelayCommand(OnFolderSelected);
            SelectFileCommand = new RelayCommand(OnFileSelected);
        }

        protected override void RunStartUp()
        {
            if (!SelectedComicsBusiness.IsAtLeastOneFolderSelected().Result)
            {
                FolderMessage =
                    "Hey welcome to SplashPage Comics.  First we need to where to find your comics.  It is best to have them all in one folder.  You can have them in many folders you just have to do more work by importing each folder.";
            }
        }

        private async void OnFileSelected()
        {
            await FileAccess.PickFile();
        }

        private async void OnFolderSelected()
        {
            var selectedFolder = await FileAccess.PickFolder();

            FolderMessage = "*****Great you selected a folder.  Now I have to do some work to import them.  Please hang on a minute, I promise not to take too long.***";

            ThreadManagement.ExecuteInSeparateThread(ReapComics, selectedFolder);
        }

        private void ReapComics(UserSelectedFolder selectedFolder)
        {
            SelectedComicsBusiness.ReapComics(selectedFolder);

            FolderMessage = "Awesome I got all you comics.";
        }
    }
}