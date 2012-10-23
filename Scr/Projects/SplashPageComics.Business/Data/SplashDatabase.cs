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
            get { return Nested.SplashDatabase; }
        }

        private static class Nested
        {
            internal static readonly SplashDatabase SplashDatabase = new SplashDatabase();

            static Nested()
            {
                SplashDatabase.Start();
            }
        }

        private SplashDatabase() {}

        public Database Database { get; set; }

        public IList<SelectedFolder> SelectedFolders { get; set; }

        private async Task CreateDatabase()
        {
            Database = await Database.CreateDatabaseAsync(DatabaseName);
        }

        private async Task LoadDatabase()
        {
            Database = await Database.OpenDatabaseAsync(DatabaseName, true);
        }

        private async void Start()
        {
            var exists = await Database.DoesDatabaseExistsAsync(DatabaseName);

            if (!exists) await CreateDatabase();
            else await LoadDatabase();

            VerifyTables();

            SelectedFolders = await Database.Table<SelectedFolder>();

            await Database.SaveAsync();
        }

        private void VerifyTables()
        {
            if (!Database.DoesTableExists(typeof(SelectedFolder))) Database.CreateTable<SelectedFolder>();
        }
    }
}