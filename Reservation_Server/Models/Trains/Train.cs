using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Reservation_Server.Models.TrainModel
{
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("train_name")]
        public string TrainName { get; set; } = string.Empty;

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("seat_count")]
        public int SeatCount { get; set; }

        [BsonElement("stations")]
        public List<Station>? Stations { get; set; }

        // Enable timestamp for train document
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }

    public class Station
    {
        [BsonElement("station_name")]
        public string StationName { get; set; } = string.Empty;

        [BsonElement("time")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Time { get; set; } = DateTime.MinValue;
    }

    public class SearchRequest
    {
       
        [BsonElement("no_of_seats")]
        public int NoOfSeats { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("start")]
        public string Start { get; set; }

        [BsonElement("end")]
        public string End { get; set; }

    }

    public class SearchResponse
    {
        [BsonElement("train_list")]
        public List<TrainList> TrainList { get; set; }

        [BsonElement("ticket_price")]
        public double TicketPrice { get; set; }

        [BsonElement("total_price")]
        public double TotalPrice { get; set; }

    }

    public class TrainList
    {
        [BsonElement("train_id")]
        public string TrainId { get; set; }

        [BsonElement("train_name")]
        public string TrainName { get; set; }

        [BsonElement("start")]
        public string Start { get; set; }

        [BsonElement("end")]
        public string End { get; set; }

        [BsonElement("start_time")]
        public DateTime StartTime { get; set; }

        [BsonElement("end_time")]
        public DateTime EndTime { get; set; }

        [BsonElement("no_of_seats")]
        public int NoOfSeats { get; set; }

        

    }



}
