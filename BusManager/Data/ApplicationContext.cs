using BusManager.Model.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BusManager.Data
{
    public class ApplicationContext
    {
        public readonly IMongoDatabase database;

        public IMongoCollection<Bus> Buses => database.GetCollection<Bus>("Buses");
        public IMongoCollection<Driver> Drivers => database.GetCollection<Driver>("Drivers");
        public IMongoCollection<Station> Stations => database.GetCollection<Station>("Stations");
        public IMongoCollection<Line> Lines => database.GetCollection<Line>("Lines");
        public IMongoCollection<Stop> Stops => database.GetCollection<Stop>("Stops");

        public ApplicationContext ()
        {
            var client = new MongoClient("");
            database = client.GetDatabase("");
        }

        private void ConfigureIndexes()
        {
            var busIndexBuilder = Builders<Bus>.IndexKeys;
            var busIndexes = new List<CreateIndexModel<Bus>>
            {
                new CreateIndexModel<Bus>(busIndexBuilder.Ascending(b => b.Plate), new CreateIndexOptions { Unique = true })
            };
            Buses.Indexes.CreateMany(busIndexes);

            var driverIndexBuilder = Builders<Driver>.IndexKeys;
            var driverIndexes = new List<CreateIndexModel<Driver>>
            {
                new CreateIndexModel<Driver>(driverIndexBuilder.Ascending(d => d.CPF), new CreateIndexOptions { Unique = true })
            };
            Drivers.Indexes.CreateMany(driverIndexes);

            var stationIndexBuilder = Builders<Station>.IndexKeys;
            var stationIndexes = new List<CreateIndexModel<Station>>
            {
                new CreateIndexModel<Station>(stationIndexBuilder.Ascending(s => s.Id))
            };
            Stations.Indexes.CreateMany(stationIndexes);

            var lineIndexBuilder = Builders<Line>.IndexKeys;
            var lineIndexes = new List<CreateIndexModel<Line>>
            {
                new CreateIndexModel<Line>(lineIndexBuilder.Ascending(l => l.Code), new CreateIndexOptions { Unique = true })
            };
            Lines.Indexes.CreateMany(lineIndexes);

            var stopIndexBuilder = Builders<Stop>.IndexKeys;
            var stopIndexes = new List<CreateIndexModel<Stop>>
            {
                new CreateIndexModel<Stop>(stopIndexBuilder.Combine(
                    stopIndexBuilder.Ascending(s => s.LineId),
                    stopIndexBuilder.Ascending(s => s.StationId)),
                    new CreateIndexOptions { Unique = true })
            };
            Stops.Indexes.CreateMany(stopIndexes);
        }
    }
}
