using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using TravelAPI.Models;

namespace TravelAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IUserStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);

        }
        public string Create(User user)
        {
            var existingUser = _users.Find(u => u.Nic == user.Nic).FirstOrDefault();
            if (existingUser != null)
            {
                return "User NIC already exists.";
            }

            user.Password = HashPassword(user.Password);

            _users.InsertOne(user);
            return "User registered successfully.";
        }

        public void Delete(string nic)
        {
            _users.DeleteOne(user => user.Nic == nic);
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _users.Find(user => user.Nic == id).FirstOrDefault();
        }

        public void Update(string id, User user)
        {
            user.Password = HashPassword(user.Password);

            _users.ReplaceOne(user => user.Nic == id, user);
        }

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
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }


        public bool VerifyLogin(string nic, string password)
        {
            var user = _users.Find(u => u.Nic == nic).FirstOrDefault();

            Console.WriteLine(user.Password);

            if (user == null)
                return false;

            return VerifyPassword(password, user.Password);
        }

        private static bool VerifyPassword(string plainTextPassword, string storeddPassword)
        {
            Console.WriteLine($"plainTextPassword {plainTextPassword}");
            Console.WriteLine($"storeddPassword {storeddPassword}");
            string hashedPassword = HashPassword(plainTextPassword);
            return hashedPassword.Equals(storeddPassword);
        }

    }
}
