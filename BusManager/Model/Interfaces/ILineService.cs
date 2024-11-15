using BusManager.Model.Entities;
using MongoDB.Bson;

namespace BusManager.Model.Interfaces
{
    public interface ILineService
    {
        bool AddLine(Line line);
        List<Line>? GetActiveLines();
        List<Line>? GetAllLines();
        Line? GetLineById(ObjectId id);
        bool UpdateLine(Line line);
        bool DeleteLine(Line line);
    }
}
