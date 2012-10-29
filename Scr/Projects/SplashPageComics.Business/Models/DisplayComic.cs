using System.Collections.Generic;
using SplashPageComics.Business.DataTypes;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace SplashPageComics.Business.Models
{
    public class DisplayComic : Comic
    {
        public StorageFile File { get; set; }

        public WriteableBitmap Cover { get; set; }

        public IList<byte[]> Images { get; set; }
    }
}