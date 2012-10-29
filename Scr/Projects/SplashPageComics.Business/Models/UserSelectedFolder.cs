using SplashPageComics.Business.DataTypes;
using Windows.Storage;

namespace SplashPageComics.Business.Models
{
    public class UserSelectedFolder : SelectedFolder
    {
        public StorageFolder StorageFolder { get; set; }
    }
}