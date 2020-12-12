namespace API.Data
{
    public interface IMusicDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}