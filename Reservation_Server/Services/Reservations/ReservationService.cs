using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Reservations;
using Reservation_Server.Models.TrainModel;

namespace Reservation_Server.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private IMongoCollection<Reservation> _reservation;

        public ReservationService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _reservation = database.GetCollection<Reservation>(settings.UsersCollectionName);

        }

        public Reservation Create(Reservation reservation)
        {
            _reservation.InsertOne(reservation);
            return reservation;
        }

        public List<Reservation> Get()
        {
            return _reservation.Find(reservation => true).ToList();

        }

        public List<Reservation> Get(string trainId, DateTime reservedDate)
        {
           return _reservation.Find(reservation => reservation.TrainId == trainId && reservation.Date == reservedDate).ToList();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
