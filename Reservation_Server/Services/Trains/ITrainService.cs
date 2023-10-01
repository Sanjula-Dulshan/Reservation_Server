using Reservation_Server.Models.TrainModel;

namespace Reservation_Server.Services.TrainService
{
    public interface ITrainService
    {
        List<Train> Get();
        Train Get(string id);
        SearchResponse GetAvailableTrains(SearchRequest searchRequest);
        Train Create(Train train);
        void Update(string id, Train train);
        void Delete(string id);
        void UpdateStatus(string id);
    }
}
