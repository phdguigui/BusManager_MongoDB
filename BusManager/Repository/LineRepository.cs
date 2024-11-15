using BusManager.Data;
using BusManager.Model.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusManager.Repository
{
    public static class LineRepository
    {
        private static readonly IMongoCollection<Line> _lineCollection;
        private static readonly IMongoCollection<Stop> _stopCollection;

        static LineRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _lineCollection = database.GetCollection<Line>("Lines");
            _stopCollection = database.GetCollection<Stop>("Stops");
        }

        public static List<Line> GetAllLines()
        {
            return _lineCollection.Find(FilterDefinition<Line>.Empty).ToList();
        }

        public static List<Line> GetActiveLines()
        {
            var filter = Builders<Line>.Filter.Eq(line => line.Active, true);
            return _lineCollection.Find(filter).ToList();
        }

        public static Line GetLineById(ObjectId id)
        {
            var filter = Builders<Line>.Filter.Eq(line => line.Id, id);
            return _lineCollection.Find(filter).FirstOrDefault();
        }

        public static bool AddLine(Line line)
        {
            _lineCollection.InsertOne(line);
            return true;
        }

        public static bool UpdateLine(Line line)
        {
            var filter = Builders<Line>.Filter.Eq(l => l.Id, line.Id);
            var result = _lineCollection.ReplaceOne(filter, line);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public static bool DeleteLine(Line line)
        {
            var filter = Builders<Line>.Filter.Eq(l => l.Id, line.Id);
            var result = _lineCollection.DeleteOne(filter);

            _stopCollection.DeleteMany(x => x.LineId == line.Id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public static Line GetLastLine()
        {
            return _lineCollection.Find(FilterDefinition<Line>.Empty)
                                  .SortByDescending(l => l.Id)
                                  .FirstOrDefault();
        }
    }
}
