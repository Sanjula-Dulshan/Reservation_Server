using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservation_Server.Models.Inquiries
{
    [BsonIgnoreExtraElements]
    public class Inquiry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_Id")]
        public string UserId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("telephone")]
        public string Telephone { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Pending";

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

