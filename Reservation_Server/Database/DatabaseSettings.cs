/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

namespace Reservation_Server.Database
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; set; } = string.Empty;
        public string TrainsCollectionName { get; set; } = string.Empty;
        public string ReservationsCollectionName { get; set; } = string.Empty;
        public string RoutesCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
