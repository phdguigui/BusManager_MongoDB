using BusManager.Model.Entities;
using MongoDB.Bson;

namespace BusManager.Model.Interfaces
{
    public interface IStopService
    {
        public bool AddStop(Stop stop);
        public bool AddManyStop(List<Stop> stopList);
        public bool DeleteStop(Stop stop);
        public List<Stop> GetRouteLine(ObjectId lineId);
        public Stop? GetStop(Stop stop);
    }
}
