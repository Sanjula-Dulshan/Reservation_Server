using TravelAPI.Models;

namespace TravelAPI.Services
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string nic);
        User Create(User user);
        void Update(string nic, User user);
        void Delete(string nic);
        bool VerifyLogin(string nic, string password);




    }
}
