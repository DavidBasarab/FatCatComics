using System.Threading.Tasks;
using SplashPageComics.Business.DataTypes;

namespace SplashPageComics.Business.Storage
{
    public interface FileAccess
    {
        Task<string> PickFile();

        Task<SelectedFolder> PickFolder();
    }
}