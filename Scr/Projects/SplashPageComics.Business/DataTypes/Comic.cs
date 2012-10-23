using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#if NETFX_CORE
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media.Imaging;
#endif


namespace SplashPageComics.Business.DataTypes
{
    public class Comic : SupportsPropertyChanges
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(value, ref name); }
        }

        private WriteableBitmap coverageImage;

        public WriteableBitmap CoverageImage
        {
            get { return coverageImage; }
            set { SetProperty(value, ref coverageImage); }
        }
    }
}
