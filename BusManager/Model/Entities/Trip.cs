using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BusManager.Model.Entities
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId BusId { get; set; }
        [BsonIgnoreIfNull]
        public Bus Bus { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId DriverId { get; set; }
        [BsonIgnoreIfNull]
        public Driver Driver { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId LineId { get; set; }
        [BsonIgnoreIfNull]
        public Line Line { get; set; }

        [BsonRequired]
        public DateTime StartTime { get; set; }

        [BsonRequired]
        public DateTime EndTime { get; set; }
    }
}
