using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinRTDatabase;

namespace Spikes
{
    // Found
    // http://dotnetspeak.com/index.php/2012/04/update-to-winrt-database-project-2/
    public class DatabaseTestStuff : PropertyChangedCore
    {
        private Database database;

        public string DatabaseName
        {
            get { return "FatCatComic_SPIKE_456"; }
        }

        public IList<SpikeDBObject> Comics { get; set; }

        public async Task CreateDummyComic()
        {
            var dbObject = new SpikeDBObject
            {
                FileLocation = string.Format("File Number {0}", DateTime.Now.Ticks),
                CollectionName = "Fantastic Four",
                HasBeenRead = false,
                LastTimeRead = DateTime.Now,
                NumberOfPages = 14,
                Title = string.Format("Title: {0}", DateTime.Now.Ticks)
            };

            Comics.Add(dbObject);

            await database.SaveAsync();
        }

        public async Task DeleteFirstComic()
        {
            if (Comics.Count == 0) return;

            Comics.RemoveAt(0);

            await database.SaveAsync();
        }

        public SpikeDBObject FindComicByTitle(string title)
        {
            return Comics.FirstOrDefault(c => c.Title.Contains(title));
        }

        public async Task RunTest()
        {
            var exists = await Database.DoesDatabaseExistsAsync(DatabaseName);

            if (!exists)
            {
                database = await Database.CreateDatabaseAsync(DatabaseName);

                database.CreateTable<SpikeDBObject>();

                var table = await database.Table<SpikeDBObject>();

                await database.SaveAsync();

                // Does this order matter
                Comics = table;
            }
            else
            {
                database = await Database.OpenDatabaseAsync(DatabaseName, true, StorageLocation.Local);

                Comics = await database.Table<SpikeDBObject>();
            }
        }

        public IList<SpikeDBObject> SimpleComics()
        {
            return Comics.ToList();
        }
    }
}