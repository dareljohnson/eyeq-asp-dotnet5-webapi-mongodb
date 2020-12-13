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
    public class SongController : ControllerBase
    {
        private readonly IMusicService _db;

        public SongController(IMusicService service)
        {
            _db = service;
        }

        // GET: api/song/all
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            var songs = await _db.LoadRecords<Song>("songs");

            if (songs.Any()) return songs;
      
            return NoContent();
        }

        // POST /api/song/add
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Song>> AddSong([FromBody]SongDto songDto)
        {
            if (songDto.SongTitle == string.Empty) return BadRequest();

            Song song = new Song
            {
                SongTitle = songDto.SongTitle ?? string.Empty,
                StageName = songDto.StageName ?? string.Empty,
                Duration = songDto.Duration != null ? songDto.Duration : new double()
            };

            if (song != null)
                await _db.InsertRecord("songs",song);

            return NoContent();
        }

        // PUT /api/song/edit
        [HttpPut("{id:length(36)}")]
        [Route("edit")]
        public async Task<ActionResult<Song>> EditSong([FromBody] SongDto songDto)
        {
            if (songDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var song = await _db.LoadRecordById<Song>("songs", songDto.Id);

            if (song == null)
            {
                return NotFound();
            }

            await _db.UpsertRecord("songs", songDto.Id, songDto);

            return NoContent();
        }

        // DELETE /api/song/delete
        [HttpDelete("{id:length(36)}")]
        [Route("delete")]
        public async Task<ActionResult<Song>> DeleteSong([FromBody] SongDto songDto)
        {
            if (songDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var song = await _db.LoadRecordById<Song>("songs", songDto.Id);

            if (song == null)
            {
                return NotFound();
            }

            await _db.DeleteRecord<Song>("songs", songDto.Id);

            return NoContent();
        }
    }
}
