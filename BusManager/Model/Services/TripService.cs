using BusManager.Model.Entities;
using BusManager.Repository;
using MongoDB.Bson;

namespace BusManager.Model.Services
{
    public class TripService
    {
        private TripRepository _tripRepository = new();
        public List<Trip> GetAllTrips()
        {
            return _tripRepository.GetAllTrips();
        }
        public bool AddTrip(Trip trip)
        {
            return _tripRepository.AddTrip(trip);
        }
        public Trip? GetTripById(ObjectId id)
        {
            return _tripRepository.GetTripById(id);
        }
        public bool UpdateTrip(Trip trip)
        {
            return _tripRepository.UpdateTrip(trip);
        }
        public bool DeleteTrip(Trip trip)
        {
            return _tripRepository.DeleteTrip(trip.Id);
        }
        public List<Trip>? GetTripsByDriverId(ObjectId driverId)
        {
            return _tripRepository.GetTripsByDriverId(driverId);
        }
        public List<Trip>? GetTripsByBusId(ObjectId busId)
        {
            return _tripRepository.GetTripsByBusId(busId);
        }
        public List<Trip>? GetTripsByLineId(ObjectId lineId)
        {
            return _tripRepository.GetTripsByLineId(lineId);
        }
    }
}