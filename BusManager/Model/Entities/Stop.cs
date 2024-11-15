using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BusManager.Model.Entities
{
    public class Stop
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("LineId")]
        public ObjectId LineId { get; set; }
        [BsonElement("StationId")]
        public ObjectId StationId { get; set; }
        [BsonIgnoreIfNull]
        public Station Station { get; set; }
        [BsonElement("Hour")]
        public DateTime Hour { get; set; }
    }
}
