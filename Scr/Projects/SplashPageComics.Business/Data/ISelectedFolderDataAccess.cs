using System.Threading.Tasks;

namespace SplashPageComics.Business.Data
{
    public interface ISelectedFolderDataAccess
    {
        Task<int> NumberOfSelectedFolders();
    }
}