namespace API.Data
{
    public class MusicDatabaseSettings : IMusicDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        //public string AlbumCollectionName { get; set; }
        //public string SongCollectionName { get; set; }
        //public string PlayListCollectionName { get; set; }
        //public string ArtistCollectionName { get; set; }
    }
}