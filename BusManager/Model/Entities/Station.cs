using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BusManager.Model.Entities
{
    public class Station
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Number")]
        public string Number { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }

        [BsonIgnoreIfNull]
        public ICollection<Stop> Stops { get; set; }
    }
}
