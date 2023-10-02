using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Users;
using System.Security.Cryptography;
using System.Text;

namespace Reservation_Server.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;


        // Constructor for UserService: Initializes database and user collection.
        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);

        }

        //creates a new user and registers them in the system.
        public string Create(User user)
        {
            var existingUser = _users.Find(u => u.Nic == user.Nic).FirstOrDefault();

            // If a user with the same NIC exists, return a message
            if (existingUser != null)
            {
                return "User NIC already exists.";
            }

            // Hash the user's password before storing it in the database for security.
            user.Password = HashPassword(user.Password);

            // Insert the user into the database.
            _users.InsertOne(user);
            return "User registered successfully.";
        }

        // Delete the user with the specified NIC
        public void Delete(string nic)
        {
            _users.DeleteOne(user => user.Nic == nic);
        }

        // Retrieves a list of all users from the database
        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        // Retrieves a user given NIC
        public User Get(string nic)
        {
            return _users.Find(user => user.Nic == nic).FirstOrDefault();
        }

        // Updates the user with the specified NIC
        public void Update(string nic, User user)
        {
            user.Password = HashPassword(user.Password);

            _users.ReplaceOne(user => user.Nic == nic, user);
        }

        // Updates a user's status (active/inactive) based on the provided NIC
        public string UpdateStatus(string nic)
        {
            
            var user = _users.Find(user => user.Nic == nic).FirstOrDefault();
            var status = !user.IsActive;
            _users.UpdateOne(user => user.Nic == nic, Builders<User>.Update.Set("IsActive", status));

            if (status)
            {
                return "activated";
            }
            else
            {
                return "deactivated";
            }
        }

        private static string HashPassword(string password)
        {
            // Hash the password using SHA256
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert the byte array to a hexadecimal string
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        // Checks if the provided NIC and password match a user in the database
        public string Login(string nic, string password)
        {
            // Find the user based on the provided NIC.
            var user = _users.Find(u => u.Nic == nic).FirstOrDefault();

            if (user == null)
                return "null";

            // If the user is inactive, return "deactivated".
            if (!user.IsActive)
                return "deactivated";
            var isVerified =  VerifyPassword(password, user.Password);

            if (isVerified)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        // Verifies the provided password against the stored password
        private static bool VerifyPassword(string plainTextPassword, string storeddPassword)
        {
            Console.WriteLine($"plainTextPassword {plainTextPassword}");
            Console.WriteLine($"storeddPassword {storeddPassword}");
            string hashedPassword = HashPassword(plainTextPassword);
            return hashedPassword.Equals(storeddPassword);
        }

    }
}
