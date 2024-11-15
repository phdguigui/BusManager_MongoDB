using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BusManager.Model.Entities
{
    public class Line
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Code")]
        public string Code { get; set; }
        [BsonElement("Origin")]
        public string Origin { get; set; }
        [BsonElement("Destiny")]
        public string Destiny { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }

        [BsonIgnoreIfNull]
        public ICollection<Stop> Stops { get; set; }
    }
}
