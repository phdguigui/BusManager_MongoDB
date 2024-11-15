using BusManager.Data;
using BusManager.Model.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusManager.Repository
{
    public static class StopRepository
    {
        private static readonly IMongoCollection<Stop> _stopCollection;
        private static readonly IMongoCollection<Station> _stationCollection;

        static StopRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _stopCollection = database.GetCollection<Stop>("Stops");
            _stationCollection = database.GetCollection<Station>("Stations");
        }

        public static bool AddStop(Stop stop)
        {
            _stopCollection.InsertOne(stop);
            return true;
        }

        public static bool DeleteStop(Stop stop)
        {
            var filter = Builders<Stop>.Filter.Eq(s => s.StationId, stop.StationId) &
                         Builders<Stop>.Filter.Eq(s => s.LineId, stop.LineId);
            var result = _stopCollection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public static bool AddManyStops(List<Stop> stops)
        {
            _stopCollection.InsertMany(stops);
            return true;
        }

        public static List<Stop> GetRouteLine(ObjectId lineId)
        {
            var filter = Builders<Stop>.Filter.Eq(stop => stop.LineId, lineId);
            var stopList = _stopCollection.Find(filter).SortBy(stop => stop.Hour).ToList();

            foreach(Stop stop in stopList)
            {
                var station = _stationCollection.Find(x => x.Id == stop.StationId).FirstOrDefault();
                stop.Station = station;
            }

            return stopList;
        }

        public static Stop GetStop(Stop stop)
        {
            var filter = Builders<Stop>.Filter.Eq(s => s.StationId, stop.StationId) &
                         Builders<Stop>.Filter.Eq(s => s.LineId, stop.LineId);
            return _stopCollection.Find(filter).FirstOrDefault();
        }

        public static List<Stop> GetStopsByStation(ObjectId stationId)
        {
            var filter = Builders<Stop>.Filter.Eq(stop => stop.StationId, stationId);
            return _stopCollection.Find(filter).ToList();
        }
    }
}
