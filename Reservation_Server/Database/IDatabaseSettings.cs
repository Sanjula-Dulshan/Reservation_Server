namespace Reservation_Server.Database
{
    public interface IDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string TrainsCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
