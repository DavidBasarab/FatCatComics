using System;
using WinRTDatabase;

namespace SplashPageComics.Business.DataTypes
{
    public class SelectedFolder : PropertyChangedCore
    {
        private DateTime dateAdded;
        private string folderLocation;

        public string FolderLocation
        {
            get { return folderLocation; }
            set { SetProperty(value, ref folderLocation); }
        }

        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { SetProperty(value, ref dateAdded); }
        }
    }
}