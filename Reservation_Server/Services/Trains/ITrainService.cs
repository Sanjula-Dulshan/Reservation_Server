using Reservation_Server.Models.TrainModel;

/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

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
        string UpdateStatus(string id);
    }
}
