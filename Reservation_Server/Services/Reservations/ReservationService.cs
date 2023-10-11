/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

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

        // Constructor for ReservationService: Initializes database and reservation collection.
        public ReservationService(IDatabaseSettings settings, IMongoClient mongoClient,ITrainRouteService trainRouteService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _reservations = database.GetCollection<Reservation>(settings.ReservationsCollectionName);
            _trains = database.GetCollection<Train>(settings.TrainsCollectionName);

            this.trainRouteService = trainRouteService;

        }

        // Creates a new reservation 
        public Reservation Create(Reservation reservation)
        {
            _reservations.InsertOne(reservation);
            return reservation;
        }

        // Retrieves a list of all reservations 
        public List<Reservation> Get()
        {
            return _reservations.Find(reservation => true).ToList();

        }

        // Retrieves a list of all reservations for a given train on a given date
        public List<Reservation> Get(string trainId, DateTime reservedDate) { 

            Console.WriteLine($"trainId>> {trainId} reservedDate {reservedDate}");

            List<Reservation> values = _reservations.Find(reservation => reservation.TrainId == trainId && reservation.Date.Date == reservedDate.Date).ToList();

            Console.WriteLine($"values {values.Count}");
            return values;
        }

        // Retrieves a reservation given ID
        public Reservation Get(string id)
        {
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault();
        }

        // Deletes the reservation with the specified ID
        public void Delete(string id)
        {
            _reservations.DeleteOne(reservation => reservation.Id == id);
        }

        // Updates the reservation with the specified ID
        public object Update(string id, Reservation reservation)
        {

            int price = trainRouteService.GetTripPrice(reservation.FromStation, reservation.ToStation);

            // Calculate the total number of reserved seats for the train on the given date.
            var t = Get(reservation.TrainId, reservation.Date);
            int reservedSeats = t.Sum(res => res.NoOfSeats);
            var train = _trains.Find(train => train.Id == reservation.TrainId).FirstOrDefault();

            // Calculate the number of available seats for the train.
            int availableSeats = train.SeatCount - reservedSeats;

            if (availableSeats < reservation.NoOfSeats)
            {
                return $"Only {availableSeats} seats available.";
            }
            reservation.TotalPrice = price * reservation.NoOfSeats;
            _reservations.ReplaceOne(reservation => reservation.Id == id, reservation);
            return reservation;
        }

        public int GetByTrainId(string trainId)
        {
            return _reservations.Find(reservation => reservation.TrainId == trainId).ToList().Count;
        }
        
    }
}
