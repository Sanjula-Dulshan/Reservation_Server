using Reservation_Server.Models.Reservations;

namespace Reservation_Server.Services.Reservations
{
    public interface IReservationService
    {
        List<Reservation> Get();
        List <Reservation> Get(string trainId, DateTime reservedDate);
        Reservation Get(string id);
        Reservation Create(Reservation reservation);
        object Update(string id, Reservation reservation);
        void Delete(string id);
        public int GetByTrainId(string trainId);


    }
}
