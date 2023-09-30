using Reservation_Server.Models.Routes;

namespace Reservation_Server.Services.Routes
{
    public interface ITrainRouteService
    {
        int GetTripPrice(string start, string end);
    }
}
