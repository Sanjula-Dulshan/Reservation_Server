using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservation_Server.Models.Reservations
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_Id")]
        public string UserId { get; set; }

        [BsonElement("train_Id")]
        public string TrainId { get; set; }

        [BsonElement("route_Id")]
        public string RouteId { get; set; }

        [BsonElement("no_of_seats")]
        public string NoOfSeats { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("total_price")]
        public string TotalPrice { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
