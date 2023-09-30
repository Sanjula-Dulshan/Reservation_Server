using Reservation_Server.Models.Reservations;

namespace Reservation_Server.Services.Reservations
{
    public interface IReservationService
    {
        List<Reservation> Get();
        Reservation Get(string id);
        Reservation Create(Reservation reservation);
        void Update(string id, Reservation reservation);
        void Remove(string id);

    }
}
