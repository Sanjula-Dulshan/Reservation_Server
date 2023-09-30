using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservation_Server.Models.Users
{
    [BsonIgnoreExtraElements]
    public class User
    {

        [BsonId]
        [BsonElement("nic")]
        public string Nic { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("isTraveler")]
        public bool IsTraveler { get; set; } = true;

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
       
  
    }
}
