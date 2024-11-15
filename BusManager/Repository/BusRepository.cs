using BusManager.Data;
using BusManager.Model.Entities;
using MongoDB.Driver;

namespace BusManager.Repository
{
    public static class BusRepository
    {
        private static readonly IMongoCollection<Bus> _busCollection;

        static BusRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _busCollection = database.GetCollection<Bus>("Buses");
        }

        public static List<Bus> GetAllBuses()
        {
            return _busCollection.Find(FilterDefinition<Bus>.Empty).ToList();
        }

        public static List<Bus> GetActiveBuses()
        {
            var filter = Builders<Bus>.Filter.Eq(bus => bus.Active, true);
            return _busCollection.Find(filter).ToList();
        }

        public static Bus GetBusByPlate(string plate)
        {
            var filter = Builders<Bus>.Filter.Eq(bus => bus.Plate, plate);
            return _busCollection.Find(filter).FirstOrDefault();
        }

        public static bool AddBus(Bus bus)
        {
            _busCollection.InsertOne(bus);
            return true;
        }

        public static bool UpdateBus(Bus bus)
        {
            var filter = Builders<Bus>.Filter.Eq(b => b.Id, bus.Id);
            var result = _busCollection.ReplaceOne(filter, bus);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public static bool DeleteBus(Bus bus)
        {
            var filter = Builders<Bus>.Filter.Eq(b => b.Id, bus.Id);
            var result = _busCollection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
