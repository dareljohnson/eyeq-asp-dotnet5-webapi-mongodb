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
    public class ArtistController : ControllerBase
    {
        private readonly IMusicService _db;

        public ArtistController(IMusicService service)
        {
            _db = service;
        }

        // GET: api/artist/all
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Artist>>> GetArtists()
        {
            var artists = await _db.LoadRecords<Artist>("artists");

            if (artists.Any()) return artists;

            return NoContent();
        }

        // POST /api/artist/add
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Artist>> AddArtist([FromBody] ArtistDto artistDto)
        {
            if (artistDto.StageName == string.Empty) return BadRequest();

            Artist artist = new Artist
            {
                StageName = artistDto.StageName ?? string.Empty,
                NumberOfTracks = artistDto.NumberOfTracks != null ? artistDto.NumberOfTracks : new int()
            };

            if (artist != null)
                await _db.InsertRecord("artists", artist);

            return NoContent();
        }

        // PUT /api/artist/edit
        [HttpPut("{id:length(36)}")]
        [Route("edit")]
        public async Task<ActionResult<Artist>> EditSong([FromBody] ArtistDto artistDto)
        {
            if (artistDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var artist = await _db.LoadRecordById<Artist>("artists", artistDto.Id);

            if (artist == null)
            {
                return NotFound();
            }

            await _db.UpsertRecord("artists", artistDto.Id, artistDto);

            return NoContent();
        }

        // DELETE /api/artist/delete
        [HttpDelete("{id:length(36)}")]
        [Route("delete")]
        public async Task<ActionResult<Artist>> DeleteSong([FromBody] ArtistDto artistDto)
        {
            if (artistDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var artist = await _db.LoadRecordById<Artist>("artists", artistDto.Id);

            if (artist == null)
            {
                return NotFound();
            }

            await _db.DeleteRecord<Artist>("artists", artistDto.Id);

            return NoContent();
        }
    }
}
