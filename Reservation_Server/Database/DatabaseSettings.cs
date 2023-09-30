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
