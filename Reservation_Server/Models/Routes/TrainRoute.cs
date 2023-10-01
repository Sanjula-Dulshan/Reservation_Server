using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservation_Server.Models.Routes
{
    public class TrainRoute
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("start")]
        public string Start { get; set; }

        [BsonElement("end")]
        public string End { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }
    }
}
