using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Errors;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMusicService _db;

        public AlbumController(IMusicService service)
        {
            _db = service;
        }

        // GET: api/album/all
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Album>>> GetAlbums()
        {
            var albums = await _db.LoadRecords<Album>("albums");

            if (albums.Any()) return albums;

            return NoContent();
        }

        // POST /api/album/add
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Album>> AddAlbum([FromBody] AlbumDto albumDto)
        {
            if (albumDto.Title == string.Empty) return BadRequest();

            List<Song> songs = new List<Song>();

            foreach (var songItem in albumDto.Songs)
            {
                songs.Add(new Song
                {
                    Id = Guid.NewGuid(),
                    SongTitle = songItem.SongTitle,
                    StageName = songItem.StageName,
                    Duration = songItem.Duration
                });
            }

            Album album = new Album
            {
                Title = albumDto.Title ?? string.Empty,
                Artist = new Artist
                {
                    Id = Guid.NewGuid(),
                    StageName = albumDto.Artist.StageName !=null ? albumDto.Artist.StageName : string.Empty,
                    NumberOfTracks = albumDto.Artist.NumberOfTracks !=null ? albumDto.Artist.NumberOfTracks : new int()
                },
                RecordLabel = albumDto.RecordLabel !=null ? albumDto.RecordLabel : string.Empty,
                ReleaseDate = albumDto.ReleaseDate !=null ? albumDto.ReleaseDate : new DateTime(),
                Songs = new List<Song>(songs)
              
            };

            if (album != null)
                await _db.InsertRecord("albums", album);

            foreach (var songItem in songs)
            {
                Song song = new Song
                {
                    SongTitle = songItem.SongTitle,
                    StageName = album.Artist.StageName,
                    Duration = songItem.Duration
                };
                await _db.InsertRecord("songs", song);
            }
            

            return NoContent();
        }

        // PUT /api/album/edit
        [HttpPut("{id:length(36)}")]
        [Route("edit")]
        public async Task<ActionResult<Album>> EditAlbum([FromBody] AlbumDto albumDto)
        {
            if (albumDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }
            
            var album = await _db.LoadRecordById<Album>("albums", albumDto.Id);

            if (album == null)
            {
                return NotFound();
            }

            List<Song> songs = new List<Song>();

            foreach (var songItem in albumDto.Songs)
            {
                if(songItem.Id == Guid.Empty)
                    songs.Add(new Song
                    {
                        Id =  Guid.NewGuid(),
                        SongTitle = songItem.SongTitle,
                        Duration = songItem.Duration
                    });
            }

            AlbumDto alb = new AlbumDto
            {
                Title = albumDto.Title,
                Artist = new Artist
                {
                    StageName = albumDto.Artist.StageName,
                    NumberOfTracks = albumDto.Artist.NumberOfTracks
                },
                RecordLabel = albumDto.RecordLabel,
                ReleaseDate = albumDto.ReleaseDate,
                Songs = new List<Song>(songs)
            };

            await _db.UpsertRecord("albums", albumDto.Id, alb);

            return NoContent();
        }

        // DELETE /api/album/delete
        [HttpDelete("{id:length(36)}")]
        [Route("delete")]
        public async Task<ActionResult<Album>> DeleteAlbum([FromBody] AlbumDto albumDto)
        {
            if (albumDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var album = await _db.LoadRecordById<Album>("albums", albumDto.Id);

            if (album == null)
            {
                return NotFound();
            }

            await _db.DeleteRecord<Album>("albums", albumDto.Id);

            return NoContent();
        }
    }
}
