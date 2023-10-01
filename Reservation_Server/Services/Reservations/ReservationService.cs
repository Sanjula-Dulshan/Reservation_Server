using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Reservations;
using Reservation_Server.Models.TrainModel;
using Reservation_Server.Services.Routes;
using Reservation_Server.Services.TrainService;

namespace Reservation_Server.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;
        private readonly IMongoCollection<Train> _trains;
        private readonly ITrainRouteService trainRouteService;




        public ReservationService(IDatabaseSettings settings, IMongoClient mongoClient,ITrainRouteService trainRouteService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _reservations = database.GetCollection<Reservation>(settings.ReservationsCollectionName);
            _trains = database.GetCollection<Train>(settings.TrainsCollectionName);

            this.trainRouteService = trainRouteService;


        }

        public Reservation Create(Reservation reservation)
        {
            _reservations.InsertOne(reservation);
            return reservation;
        }

        public List<Reservation> Get()
        {
            return _reservations.Find(reservation => true).ToList();

        }

        public List<Reservation> Get(string trainId, DateTime reservedDate)
        {
           return _reservations.Find(reservation => reservation.TrainId == trainId && reservation.Date.Date == reservedDate.Date).ToList();
        }

        public Reservation Get(string id)
        {
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _reservations.DeleteOne(reservation => reservation.Id == id);
        }

        public object Update(string id, Reservation reservation)
        {

            int price = trainRouteService.GetTripPrice(reservation.FromStation, reservation.ToStation);
            int reservedSeats = Get(reservation.TrainId, reservation.Date).Sum(res => res.NoOfSeats);
            var train = _trains.Find(train => train.Id == reservation.TrainId).FirstOrDefault();

            int availableSeats = train.SeatCount - reservedSeats;

            if (availableSeats < reservation.NoOfSeats)
            {
                return $"Only {availableSeats} seats available.";
            }
            reservation.TotalPrice = price * reservation.NoOfSeats;
            _reservations.ReplaceOne(reservation => reservation.Id == id, reservation);
            return reservation;
        }

        
    }
}
