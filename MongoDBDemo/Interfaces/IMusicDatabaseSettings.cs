namespace API.Data
{
    public interface IMusicDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        //string AlbumCollectionName { get; set; }
        //string SongCollectionName { get; set; }
        //string PlayListCollectionName { get; set; }
        //string ArtistCollectionName { get; set; }
    }
}