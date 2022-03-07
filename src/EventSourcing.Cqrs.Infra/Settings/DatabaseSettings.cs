namespace EventSourcing.Cqrs.Infra.Settings;

public class DatabaseSettings
{
    public string EventStoreConnectionString { get; set; }
    public string PostgresConnectionString { get; set; }
}