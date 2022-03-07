namespace EventSourcing.Cqrs.Infra.Settings;

public class DatabaseSettings
{
    public string EventStoreConnectionString { get; set; }
    public string MongoConnectionString { get; set; }
    public string MongoConnectionDatabaseName { get; set; }
}