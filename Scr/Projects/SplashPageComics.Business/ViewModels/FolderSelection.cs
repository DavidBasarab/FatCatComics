using SplashPageComics.Business.Logic;

namespace SplashPageComics.Business.ViewModels
{
    public class FolderSelection : BaseViewModel
    {
        public FolderSelection()
            : this(Global.Create<MessengerService>(), Global.Create<SelectedComicsBusiness>()) {}

        public FolderSelection(MessengerService messengerService, SelectedComicsBusiness selectedComicsBusiness)
            : base(messengerService)
        {
            SelectedComicsBusiness = selectedComicsBusiness;
        }

        private SelectedComicsBusiness SelectedComicsBusiness { get; set; }
    }
}