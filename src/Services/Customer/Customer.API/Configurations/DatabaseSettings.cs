using Customer.API.Configurations.Interfaces;

namespace Customer.API.Configurations
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
