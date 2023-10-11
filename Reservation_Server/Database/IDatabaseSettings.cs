/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

namespace Reservation_Server.Database
{
    public interface IDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string TrainsCollectionName { get; set; }
        string ReservationsCollectionName { get; set; }
        string RoutesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
