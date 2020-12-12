namespace API.Data
{
    public class MusicDatabaseSettings : IMusicDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}