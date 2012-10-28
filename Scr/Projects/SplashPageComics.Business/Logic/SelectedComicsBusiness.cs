using System.Threading.Tasks;

namespace SplashPageComics.Business.Logic
{
    public interface SelectedComicsBusiness
    {
        Task<bool> IsAtLeastOneFolderSelected();
    }
}