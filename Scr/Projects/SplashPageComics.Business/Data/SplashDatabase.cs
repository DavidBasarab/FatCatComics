using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SplashPageComics.Business.DataTypes;
using WinRTDatabase;

namespace SplashPageComics.Business.Data
{
    public class SplashDatabase : DataStore
    {
        private const string DatabaseName = "SplashPageComicsDatabase";

        public static DataStore Instance
        {
            get { return Nested.instance; }
        }

        private SplashDatabase() {}

        public IList<SelectedFolder> SelectedFolders { get; set; }

        private Task CreateDatabase()
        {
            throw new NotImplementedException();
        }

        private Task LoadDatabase()
        {
            throw new NotImplementedException();
        }

        private async void Start()
        {
            var exists = await Database.DoesDatabaseExistsAsync(DatabaseName);

            if (!exists) await CreateDatabase();
            else await LoadDatabase();
        }

        private class Nested
        {
            internal static readonly SplashDatabase instance = new SplashDatabase();

            static Nested()
            {
                instance.Start();
            }
        }
    }
}