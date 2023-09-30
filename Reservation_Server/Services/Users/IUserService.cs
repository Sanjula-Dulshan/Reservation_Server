using Reservation_Server.Models.Users;

namespace Reservation_Server.Services.Users
{
    public interface IUserService
    {
        List<Models.Users.Route> Get();
        Models.Users.Route Get(string nic);
        string Create(Models.Users.Route user);
        void Update(string nic, Models.Users.Route user);
        void Delete(string nic);
        string Login(string nic, string password);
        string UpdateStatus(string nic);




    }
}
