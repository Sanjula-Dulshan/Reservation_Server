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
