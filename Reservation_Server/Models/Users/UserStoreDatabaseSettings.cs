namespace Reservation_Server.Models.Users
{
    public class UserStoreDatabaseSettings : IUserStoreDatabaseSettings
    {
        public string UserCollectionName { get; set; } = string.Empty;
        public string TrainsCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
