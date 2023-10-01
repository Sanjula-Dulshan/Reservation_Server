﻿using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Users;
using System.Security.Cryptography;
using System.Text;

namespace Reservation_Server.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);

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


        public string Login(string nic, string password)
        {
            var user = _users.Find(u => u.Nic == nic).FirstOrDefault();

            

            Console.WriteLine(user.Password);

            if (user == null)
                return "null";
            if(!user.IsActive)
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

        private static bool VerifyPassword(string plainTextPassword, string storeddPassword)
        {
            Console.WriteLine($"plainTextPassword {plainTextPassword}");
            Console.WriteLine($"storeddPassword {storeddPassword}");
            string hashedPassword = HashPassword(plainTextPassword);
            return hashedPassword.Equals(storeddPassword);
        }

    }
}
