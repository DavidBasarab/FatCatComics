using System.Threading.Tasks;
using SplashPageComics.Business.DataTypes;
using SplashPageComics.Business.Models;

namespace SplashPageComics.Business.Logic
{
    public interface SelectedComicsBusiness
    {
        Task<bool> IsAtLeastOneFolderSelected();

        Task ReapComics(UserSelectedFolder selectedFolder);
    }
}