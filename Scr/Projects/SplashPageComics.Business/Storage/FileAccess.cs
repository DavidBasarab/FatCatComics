using System.Threading.Tasks;
using SplashPageComics.Business.Models;

namespace SplashPageComics.Business.Storage
{
    public interface FileAccess
    {
        Task<string> PickFile();

        Task<UserSelectedFolder> PickFolder();
    }
}