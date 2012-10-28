using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace SplashPageComics.Business.Models
{
    public class Comic
    {
        public StorageFile File { get; set; }

        public string Name { get; set; }

        public WriteableBitmap Cover { get; set; }

        public IList<byte[]> Images { get; set; }
    }
}