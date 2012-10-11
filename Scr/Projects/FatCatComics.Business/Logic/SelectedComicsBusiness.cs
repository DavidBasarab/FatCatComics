using SplashPageComics.Business.Data;

namespace SplashPageComics.Business.Logic
{
    public interface SelectedComicsBusiness {}

    internal class SelectedComics : SelectedComicsBusiness
    {
        public SelectedComics(DataStore dataStore)
        {
            DataStore = dataStore;
        }

        public DataStore DataStore { get; set; }
    }
}