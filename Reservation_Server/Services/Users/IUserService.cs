/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

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
