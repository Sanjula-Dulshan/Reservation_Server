using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Routes;

namespace Reservation_Server.Services.Routes
{
    public class TrainRouteService : ITrainRouteService
    {
        private readonly IMongoCollection<TrainRoute> _routes;

        // Constructor for TrainRouteService: Initializes database and train route collection.
        public TrainRouteService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _routes = database.GetCollection<TrainRoute>(settings.RoutesCollectionName);

        }

        // Retrieves the price for a trip based on the start and end stations.
        public int GetTripPrice(string start, string end) {

            var route = _routes.Find(trainRoute => trainRoute.Start == start && trainRoute.End == end).FirstOrDefault();
            return route.Price;
        }
    }
}
