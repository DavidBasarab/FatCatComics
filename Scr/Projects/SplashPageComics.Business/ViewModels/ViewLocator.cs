namespace SplashPageComics.Business.ViewModels
{
    public class ViewLocator
    {
        public static FolderSelection FolderSelection { get; set; }
        public static MainViewModel MainViewModel { get; set; }

        public ViewLocator()
        {
            MainViewModel = new MainViewModel();
            FolderSelection = new FolderSelection();
        }
    }
}