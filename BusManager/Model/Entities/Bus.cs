using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusManager.Model.Entities
{
    public class Bus
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Model")]
        public string Model { get; set; }
        [BsonElement("Plate")]
        public string Plate { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }
    }
}
