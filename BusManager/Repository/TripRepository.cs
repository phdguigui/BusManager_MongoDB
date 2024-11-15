using BusManager.Data;
using BusManager.Model.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BusManager.Repository
{
    public class TripRepository
    {
        private readonly IMongoCollection<Trip> _tripCollection;
        private readonly IMongoCollection<Bus> _busCollection;
        private readonly IMongoCollection<Driver> _driverCollection;
        private readonly IMongoCollection<Line> _lineCollection;

        public TripRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _tripCollection = database.GetCollection<Trip>("Trips");
            _busCollection = database.GetCollection<Bus>("Buses");
            _driverCollection = database.GetCollection<Driver>("Drivers");
            _lineCollection = database.GetCollection<Line>("Lines");
        }

        public List<Trip> GetAllTrips()
        {
            var trips = _tripCollection.Find(_ => true).ToList();
            foreach (var trip in trips)
            {
                trip.Bus = _busCollection.Find(b => b.Id == trip.BusId).FirstOrDefault();
                trip.Driver = _driverCollection.Find(d => d.Id == trip.DriverId).FirstOrDefault();
                trip.Line = _lineCollection.Find(l => l.Id == trip.LineId).FirstOrDefault();
            }
            return trips;
        }

        public Trip? GetTripById(ObjectId id)
        {
            var trip = _tripCollection.Find(t => t.Id == id).FirstOrDefault();
            if (trip != null)
            {
                trip.Bus = _busCollection.Find(b => b.Id == trip.BusId).FirstOrDefault();
                trip.Driver = _driverCollection.Find(d => d.Id == trip.DriverId).FirstOrDefault();
                trip.Line = _lineCollection.Find(l => l.Id == trip.LineId).FirstOrDefault();
            }
            return trip;
        }

        public bool AddTrip(Trip trip)
        {
            _tripCollection.InsertOne(trip);
            return trip.Id != null;
        }

        public bool UpdateTrip(Trip trip)
        {
            var result = _tripCollection.ReplaceOne(t => t.Id == trip.Id, trip);
            return result.ModifiedCount > 0;
        }

        public bool DeleteTrip(ObjectId id)
        {
            var result = _tripCollection.DeleteOne(t => t.Id == id);
            return result.DeletedCount > 0;
        }

        public List<Trip>? GetTripsByDriverId(ObjectId driverId)
        {
            var trips = _tripCollection.Find(t => t.DriverId == driverId).ToList();
            foreach (var trip in trips)
            {
                trip.Driver = _driverCollection.Find(d => d.Id == driverId).FirstOrDefault();
            }
            return trips;
        }

        public List<Trip>? GetTripsByBusId(ObjectId busId)
        {
            var trips = _tripCollection.Find(t => t.BusId == busId).ToList();
            foreach (var trip in trips)
            {
                trip.Bus = _busCollection.Find(b => b.Id == busId).FirstOrDefault();
            }
            return trips;
        }

        public List<Trip>? GetTripsByLineId(ObjectId lineId)
        {
            var trips = _tripCollection.Find(t => t.LineId == lineId).ToList();
            foreach (var trip in trips)
            {
                trip.Line = _lineCollection.Find(l => l.Id == lineId).FirstOrDefault();
            }
            return trips;
        }
    }
}
