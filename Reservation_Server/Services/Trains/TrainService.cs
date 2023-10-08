using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.TrainModel;
using Reservation_Server.Services.Reservations;
using Reservation_Server.Services.Routes;

namespace Reservation_Server.Services.TrainService
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        private readonly ITrainRouteService trainRouteService;
        private readonly IReservationService reservationService;

        // Constructor for TrainService: Initializes database and train collection.
        public TrainService(IDatabaseSettings settings, IMongoClient mongoClient, ITrainRouteService trainRouteService, IReservationService reservationService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainsCollectionName);

            this.trainRouteService = trainRouteService;
            this.reservationService = reservationService;
        }

        // Creates a new train and registers it in the system.
        public Train Create(Train train)
        {

            _trains.InsertOne(train);
            return train;
        }

        // Retrieves a list of all trains from the database
        public List<Train> Get()
        {
            return _trains.Find(train => true).ToList();
        }

        // Retrieves a train given ID
        public Train Get(string id)
        {
            return _trains.Find(train => train.Id == id).FirstOrDefault();
        }

        // Deletes the train with the specified ID
        public string Delete(string id)
        {
            int reservationCount = reservationService.GetByTrainId(id);
            Console.WriteLine($"reservationCount {reservationCount}");
            if (reservationCount > 0)
            {
                return $"Train cannot be deleted as it has {reservationCount} reservation";
                
            }
            _trains.DeleteOne(train => train.Id == id);
            return "Train deleted successfully";
        }

        // Updates the train with the specified ID
        public void Update(string id, Train train)
        {
            _trains.ReplaceOne(train => train.Id == id, train);
        }

        // Updates the status of the train with the specified ID
        public void UpdateStatus(string id)
        {
            var train = _trains.Find(train => train.Id == id).FirstOrDefault();
            train.IsActive = !train.IsActive;
            _trains.UpdateOne(train => train.Id == id, Builders<Train>.Update.Set("IsActive", train.IsActive));
        }

        // Retrieves available trains and total price based on the provided search criteria
        public SearchResponse GetAvailableTrains(SearchRequest searchRequest)
        {
            // Get the price for the trip between the start and end stations.
            int price = trainRouteService.GetTripPrice(searchRequest.Start, searchRequest.End);

            var trains = _trains.Find(train =>
                train.Stations != null &&
                train.Stations.Any(station => station.StationName == searchRequest.Start) &&
                train.Stations.Any(station => station.StationName == searchRequest.End) &&
                train.Stations.Any(station => station.StationName == searchRequest.Start &&
                                              station.Time < train.Stations.First(s => s.StationName == searchRequest.End).Time)
            ).ToList();


           List<TrainList> availableTrainList = new();

            foreach (var train in trains)
            {
                // Calculate the number of reserved seats for the train.
                var reservedSeats = reservationService.Get(train.Id, searchRequest.Date).Sum(reservation => reservation.NoOfSeats);

                // Calculate the available seats for the train.
                var availableSeats = train.SeatCount - reservedSeats;
                if (availableSeats > 0 && availableSeats >= searchRequest.NoOfSeats)
                {
                    train.SeatCount = availableSeats;

                    TrainList availableTrains = new()
                    {
                        TrainId = train.Id,
                        TrainName = train.TrainName,
                        Start = searchRequest.Start,
                        End = searchRequest.End,
                        StartTime = train.Stations.First(station => station.StationName == searchRequest.Start).Time,
                        EndTime = train.Stations.First(station => station.StationName == searchRequest.End).Time,
                        NoOfSeats = availableSeats,
                        
                    };

                    availableTrainList.Add(availableTrains);

                }

            }

            SearchResponse searchResponse = new()
            {
                TrainList = availableTrainList,
                TicketPrice = price,
                TotalPrice = price * searchRequest.NoOfSeats
            };
          
           
            return searchResponse;

        }
    }
}
