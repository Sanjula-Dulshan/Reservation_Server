using Reservation_Server.Models.Users;

namespace Reservation_Server.Services.Users
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string nic);
        string Create(User user);
        void Update(string nic, User user);
        void Delete(string nic);
        string Login(string nic, string password);
        string UpdateStatus(string nic);




    }
}
