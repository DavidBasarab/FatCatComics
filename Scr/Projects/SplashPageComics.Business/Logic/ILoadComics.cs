using System.Collections.Generic;
using SplashPageComics.Business.Models;
using Windows.Storage;

namespace SplashPageComics.Business.Logic
{
    public interface ILoadComics
    {
        IEnumerable<DisplayComic> InitialLoadOfComics(IEnumerable<StorageFile> files);
    }
}