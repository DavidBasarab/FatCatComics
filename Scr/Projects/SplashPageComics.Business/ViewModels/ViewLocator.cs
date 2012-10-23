namespace SplashPageComics.Business.ViewModels
{
    public class ViewLocator
    {
        public static ImportComicViewModel ImportComicViewModel { get; set; }
        public static MainViewModel MainViewModel { get; set; }

        public ViewLocator()
        {
            MainViewModel = new MainViewModel();
            ImportComicViewModel = new ImportComicViewModel();
        }
    }
}