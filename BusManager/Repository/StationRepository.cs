using BusManager.Data;
using BusManager.Model.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusManager.Repository
{
    public static class StationRepository
    {
        private static readonly IMongoCollection<Station> _stationCollection;
        private static readonly IMongoCollection<Stop> _stopCollection;

        static StationRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _stationCollection = database.GetCollection<Station>("Stations");
            _stopCollection = database.GetCollection<Stop>("Stops");
        }

        public static List<Station> GetAllStations()
        {
            return _stationCollection.Find(FilterDefinition<Station>.Empty).ToList();
        }

        public static List<Station> GetActiveStations()
        {
            var filter = Builders<Station>.Filter.Eq(station => station.Active, true);
            return _stationCollection.Find(filter).ToList();
        }

        public static Station GetStationById(ObjectId id)
        {
            var filter = Builders<Station>.Filter.Eq(station => station.Id, id);
            return _stationCollection.Find(filter).FirstOrDefault();
        }

        public static bool AddStation(Station station)
        {
            _stationCollection.InsertOne(station);
            return true;
        }

        public static bool UpdateStation(Station station)
        {
            var filter = Builders<Station>.Filter.Eq(s => s.Id, station.Id);
            var result = _stationCollection.ReplaceOne(filter, station);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public static bool DeleteStation(Station station)
        {
            var filter = Builders<Station>.Filter.Eq(s => s.Id, station.Id);
            var result = _stationCollection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public static List<Station>? GetOutdatedStations()
        {
            List<Station> result = new();
            var stationList = _stationCollection.Find(x => x.Id != ObjectId.Parse("6736a8117e1e1bd1569884c1")).ToList();

            foreach (var station in stationList)
            {
                if (_stopCollection.Find(x => x.StationId == station.Id).ToList().Count() < 1)
                {
                    result.Add(station);
                } 
            }

            return result;
        }
    }
}
