using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusManager.Model.Entities
{
    public class Driver
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("CPF")]
        public string CPF { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Birthday")]
        public DateTime Birthday { get; set; }
        [BsonElement("HireDate")]
        public DateTime HireDate { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }
    }
}
