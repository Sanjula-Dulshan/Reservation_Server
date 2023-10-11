/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservation_Server.Models.Reservations
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_Id")]
        public string UserId { get; set; }

        [BsonElement("train_Id")]
        public string TrainId { get; set; }

        [BsonElement("from_station")]
        public string FromStation { get; set; }

        [BsonElement("to_station")]
        public string ToStation { get; set; }

        [BsonElement("no_of_seats")]
        public int NoOfSeats { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("total_price")]
        public double TotalPrice { get; set; }

        [BsonElement("isAgent")]
        public bool IsAgent { get; set; } = false;


        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
