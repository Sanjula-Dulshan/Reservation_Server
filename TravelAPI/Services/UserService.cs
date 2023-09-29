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
        public User Create(User user)
        {
            user.Password = HashPassword(user.Password);
            _users.InsertOne(user);
            return user;
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
            _users.ReplaceOne(user => user.Nic == id, user);
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

            if (user == null)
                return false;

            return VerifyPassword(password, user.Password);
        }

        private static bool VerifyPassword(string plainTextPassword, string storeddPassword)
        {
            string hashedPassword = HashPassword(plainTextPassword);
            return hashedPassword.Equals(storeddPassword);
        }

    }
}
