using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Reservations;

namespace Reservation_Server.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservation;


        public ReservationService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _reservation = database.GetCollection<Reservation>(settings.ReservationsCollectionName);

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
           return _reservation.Find(reservation => reservation.TrainId == trainId && reservation.Date.Date == reservedDate.Date).ToList();
        }

        public Reservation Get(string id)
        {
            return _reservation.Find(reservation => reservation.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _reservation.DeleteOne(reservation => reservation.Id == id);
        }

        public void Update(string id, Reservation reservation)
        {
            _reservation.ReplaceOne(reservation => reservation.Id == id, reservation);
        }

        
    }
}
