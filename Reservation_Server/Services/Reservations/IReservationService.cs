/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

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
