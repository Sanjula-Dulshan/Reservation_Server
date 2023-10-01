using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Reservations;
using Reservation_Server.Models.TrainModel;
using Reservation_Server.Models.Users;
using Reservation_Server.Services.Reservations;
using Reservation_Server.Services.Routes;
using System.Collections.Generic;

namespace Reservation_Server.Services.TrainService
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        private readonly ITrainRouteService trainRouteService;
        private readonly IReservationService reservationService;


        public TrainService(IDatabaseSettings settings, IMongoClient mongoClient, ITrainRouteService trainRouteService, IReservationService reservationService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainsCollectionName);

            this.trainRouteService = trainRouteService;
            this.reservationService = reservationService;
        }

        public Train Create(Train train)
        {

            _trains.InsertOne(train);
            return train;
        }

        public List<Train> Get()
        {
            return _trains.Find(train => true).ToList();
        }

        public Train Get(string id)
        {
            return _trains.Find(train => train.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _trains.DeleteOne(train => train.Id == id);
        }

        public void Update(string id, Train train)
        {
            _trains.ReplaceOne(train => train.Id == id, train);
        }

        public void UpdateStatus(string id)
        {
            var train = _trains.Find(train => train.Id == id).FirstOrDefault();
            train.IsActive = !train.IsActive;
            _trains.UpdateOne(train => train.Id == id, Builders<Train>.Update.Set("IsActive", train.IsActive));
        }

        public SearchResponse GetAvailableTrains(SearchRequest searchRequest)
        {

            int price = trainRouteService.GetTripPrice(searchRequest.Start, searchRequest.End);
            /* var trains = _trains.Find(train => train.Stations.Any(station => station.StationName == searchRequest.Start) && 
             train.Stations.Any(station => station.StationName == searchRequest.End)).ToList();
            */

            // Get trains by going through all stations and check if departure and arrival are in the list of stations.
            var trains = _trains.Find(train =>
                train.Stations != null &&
                train.Stations.Any(station => station.StationName == searchRequest.Start) &&
                train.Stations.Any(station => station.StationName == searchRequest.End) &&
                train.Stations.Any(station => station.StationName == searchRequest.Start &&
                                              station.Time < train.Stations.First(s => s.StationName == searchRequest.End).Time)
            ).ToList();

            Console.WriteLine($"trains {trains}");

           List<TrainList> availableTrainList = new List<TrainList>();

            foreach (var train in trains)
            {
                var reservedSeats = reservationService.Get(train.Id, searchRequest.Date).Sum(reservation => reservation.NoOfSeats);
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
