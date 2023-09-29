using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAPI.Models
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

        [BsonElement("isAdmin")] 
        public bool IsAdmin { get; set; }

        public User()
        {
            // Set the default value for isAdmin
            IsAdmin = false;
        }
    }
}
