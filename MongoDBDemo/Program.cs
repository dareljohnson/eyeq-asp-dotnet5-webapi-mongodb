using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await SeedData.SeedAlbums();
            //await SeedData.SeedSongs();
            //await SeedData.SeedArtists();

            //MusicService db = new MusicService("music_database");

            //PlayList playList = new PlayList
            //{ 
            //    CreatedAt = DateTime.Now,
            //    Album = new Album
            //    {
            //        Title = "Slippery When Wet",
            //        ReleaseDate = new DateTime(2001, 3, 15),
            //        Artist = new Artist
            //        {
            //            StageName = "Bon Jovi"
            //        },
            //        RecordLabel = "Mercury",
            //        Songs = new List<Song>
            //        {
            //            new Song { SongTitle = "Livin' on a Prayer"}
            //        }
            //    }
            //};

            //Artist artist = new Artist { StageName = "Bon Jovi", NumberOfTracks = 43 };
            //Song songTitle = new Song { SongTitle = "Wanted Dead or Alive", Duration = 5.4 };
            //Album albumName = new Album { Title = "Slippery When Wet", RecordLabel = "Mercury", Artist = new Artist {StageName = "Bon Jovi" } };

            //await db.InsertRecord("playlists", playList);
            //await db.InsertRecord("artists", artist);
            //await db.InsertRecord("songs", songTitle);
            //await db.InsertRecord("albums", albumName);


            //MongoCRUD db = new MongoCRUD("AddressBook");

            //var recs = db.LoadRecords<PlayListDto>("playlist").Result.ToList();

            //foreach (var rec in recs)
            //{
            //    var stageName = rec.Album.Artist.StageName ?? string.Empty;
            //    var albumTitle = rec.Album.Title ?? string.Empty;

            //    Console.WriteLine($"{ stageName } { albumTitle }");

            //    var SongList = rec.Album.Songs.Any() ? rec.Album.Songs : new List<Song>() ;
            //    foreach (var song in SongList)
            //    {
            //        Console.WriteLine($" { song.SongTitle }");
            //    }

            //    Console.WriteLine();
            //}


            //var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("34263cdf-bc5f-4747-b39c-c4522cbad08c"));

            //await db.DeleteRecord<PersonModel>("Users", oneRec.Id);

            //oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            //await db.UpsertRecord("Users", oneRec.Id, oneRec);

            //Console.WriteLine(oneRec.PrimaryAddress.City);

            //var recs = db.LoadRecords<PersonModel>("Users").Result.ToList();

            //foreach (var rec in recs)
            //{
            //    Console.WriteLine($"{ rec.Id}: { rec.FirstName} {rec.LastName}");

            //    if (rec.PrimaryAddress != null)
            //    {
            //        Console.WriteLine(rec.PrimaryAddress.City);
            //    }
            //}


            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};

            //await db.InsertRecord("Users", person);

            //await db.InsertRecord<PersonModel>("Users", new PersonModel { FirstName = "Bob", LastName = "White" });

            Console.ReadLine();
        }
    }
}
