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
        public bool IsTraveler { get; set; } = false;

        [BsonElement("isAgent")]
        public bool IsAgent { get; set; } = false;

        [BsonElement("isBackOffice")]
        public bool IsBackOffice { get; set; } = false;

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
       
  
    }

    public class LoginRequest
    {
        [BsonElement("nic")]
        public string Nic { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
