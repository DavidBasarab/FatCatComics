using System.Windows.Input;
using SplashPageComics.Business.Logic;

namespace SplashPageComics.Business.ViewModels
{
    public class ImportComicViewModel : BaseViewModel
    {
        public ImportComicViewModel()
            : this(Global.Create<MessengerService>(), Global.Create<SelectedComicsBusiness>()) {}

        public ImportComicViewModel(MessengerService messengerService, SelectedComicsBusiness selectedComicsBusiness)
            : base(messengerService)
        {
            SelectedComicsBusiness = selectedComicsBusiness;
        }

        public override void RegisterForMessages()
        {
            SelectFolderCommand = OnFolderSelected();
            SelectFileCommand = OnFileSelected();
        }

        private ICommand OnFileSelected()
        {
            throw new System.NotImplementedException();
        }

        private ICommand OnFolderSelected()
        {
            throw new System.NotImplementedException();
        }

        public ICommand SelectFolderCommand { get; set; }
        public ICommand SelectFileCommand { get; set; }

        private SelectedComicsBusiness SelectedComicsBusiness { get; set; }
    }
}