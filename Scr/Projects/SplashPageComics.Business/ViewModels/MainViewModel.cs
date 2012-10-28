using SplashPageComics.Business.Logic;
using SplashPageComics.Business.Threading;

namespace SplashPageComics.Business.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool showSelectFolder;

        public MainViewModel()
            : this(Global.Create<MessengerService>(), Global.Create<ThreadManagement>(), Global.Create<SelectedComicsBusiness>()) {}

        public MainViewModel(MessengerService messengerService, ThreadManagement threadManagement, SelectedComicsBusiness selectedComicsBusiness)
            : base(messengerService, threadManagement)
        {
            SelectedComicsBusiness = selectedComicsBusiness;
        }

        private SelectedComicsBusiness SelectedComicsBusiness { get; set; }

        public bool ShowSelectFolder
        {
            get { return showSelectFolder; }
            set { SetProperty(value, ref showSelectFolder); }
        }

        public async void Start()
        {
            ShowSelectFolder = await SelectedComicsBusiness.IsAtLeastOneFolderSelected();
        }
    }
}