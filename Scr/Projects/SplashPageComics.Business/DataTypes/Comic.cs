using System;

namespace SplashPageComics.Business.DataTypes
{
    public class Comic : SupportsPropertyChanges
    {
        private byte[] coverageImageBytes;
        private int currentPageOne;
        private DateTime dateAdded;
        private string fullPath;
        private int lastPageRead;
        private string name;
        private int pages;
        private bool read;

        public string Name
        {
            get { return name; }
            set { SetProperty(value, ref name); }
        }

        public byte[] CoverageImageBytes
        {
            get { return coverageImageBytes; }
            set { SetProperty(value, ref coverageImageBytes); }
        }

        public string FullPath
        {
            get { return fullPath; }
            set { SetProperty(value, ref fullPath); }
        }

        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { SetProperty(value, ref dateAdded); }
        }

        public int Pages
        {
            get { return pages; }
            set { SetProperty(value, ref pages); }
        }

        public int LastPageRead
        {
            get { return lastPageRead; }
            set { SetProperty(value, ref lastPageRead); }
        }

        public bool Read
        {
            get { return read; }
            set { SetProperty(value, ref read); }
        }

        public int CurrentPageOne
        {
            get { return currentPageOne; }
            set { SetProperty(value, ref currentPageOne); }
        }
    }
}