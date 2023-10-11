/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

using Reservation_Server.Models.Routes;

namespace Reservation_Server.Services.Routes
{
    public interface ITrainRouteService
    {
        int GetTripPrice(string start, string end);
    }
}
