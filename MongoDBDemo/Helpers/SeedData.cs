using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class SeedData
    {
        static MusicService _db = new MusicService("music_database");

        public static async Task SeedAlbums()
        {
            if(_db.LoadRecords<Album>("albums").Result.Any()) return;

            var albumData = System.IO.File.ReadAllText("AlbumSeedData.json");
            var albums = JsonSerializer.Deserialize<List<Album>>(albumData);

            Album album = new Album();

            foreach (var albumItem in albums)
            {
                album.Title = albumItem.Title;
                album.Artist = new Artist { StageName = albumItem.Artist.StageName };
                album.RecordLabel = albumItem.RecordLabel;
                foreach (var songItem in albumItem.Songs)
                {
                    album.Songs.Add(new Song
                    {
                        SongTitle = songItem.SongTitle, Duration = songItem.Duration
                    });

                    await _db.InsertRecord("albums", album);
                }
            }
        }

        public static async Task SeedSongs()
        {
            if (_db.LoadRecords<Song>("songs").Result.Any()) return;

            var songData = System.IO.File.ReadAllText("SongSeedData.json");
            var songs = JsonSerializer.Deserialize<List<Song>>(songData);

            List<Song> song = new List<Song>();

            foreach (var songItem in songs)
            {
                song.Add(new Song
                {
                    SongTitle = songItem.SongTitle,
                    Duration = songItem.Duration
                });

                await _db.InsertRecord("songs", song);
            }

            
        }

        public static async Task SeedArtists()
        {
            if (_db.LoadRecords<Artist>("artists").Result.Any()) return;

            var artistData = System.IO.File.ReadAllText("ArtistSeedData.json");
            var artists = JsonSerializer.Deserialize<List<Artist>>(artistData);

            Artist artist = new Artist();

            foreach (var artistItem in artists)
            {
                artist.StageName = artistItem.StageName;
                artist.NumberOfTracks = artistItem.NumberOfTracks;

                await _db.InsertRecord("artists", artist);
            }
        }

    }
}
