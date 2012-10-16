using System.Collections.Generic;
using SplashPageComics.Business.DataTypes;

namespace SplashPageComics.Business.Data
{
    public interface DataStore
    {
        IList<SelectedFolder> SelectedFolders { get; set; }
    }
}