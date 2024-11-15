using BusManager.Model.Entities;
using MongoDB.Bson;

namespace BusManager.Model.Interfaces
{
    public interface IStationService
    {
        bool AddStation(Station station);
        List<Station>? GetActiveStations();
        List<Station>? GetAllStations();
        Station? GetStationById(ObjectId id);
        bool UpdateStation(Station station);
        bool DeleteStation(Station station);
    }
}
