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
        string Delete(string id);
        void UpdateStatus(string id);
    }
}
