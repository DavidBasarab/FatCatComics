using System;
using SplashPageComics.Business.Logic;

namespace SplashPageComics.Business.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool showSelectFolder;

        public MainViewModel()
            : this(Global.Create<MessengerService>(), Global.Create<SelectedComicsBusiness>()) {}

        public MainViewModel(MessengerService messengerService, SelectedComicsBusiness selectedComicsBusiness)
            : base(messengerService)
        {
            SelectedComicsBusiness = selectedComicsBusiness;
        }

        private SelectedComicsBusiness SelectedComicsBusiness { get; set; }

        public bool ShowSelectFolder
        {
            get { return showSelectFolder; }
            set { SetProperty(value, ref showSelectFolder); }
        }

        public void Start()
        {
            ShowSelectFolder = SelectedComicsBusiness.IsAtLeastOneFolderSelected();
        }
    }
}