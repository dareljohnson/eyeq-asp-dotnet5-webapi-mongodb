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
    public class PlayListController : ControllerBase
    {
        private readonly IMusicService _db;

        public PlayListController(IMusicService service)
        {
            _db = service;
        }

        // GET: api/playlist/all
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<PlayList>>> GetPlayLists()
        {
            var playLists = await _db.LoadRecords<PlayList>("playlists");

            if (playLists.Any()) return playLists;

            return NoContent();
        }

        // POST /api/playlist/add
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<PlayList>> AddPlayList([FromBody] PlayListDto playListDto)
        {
            if (playListDto.SongTitle == string.Empty) return BadRequest();

            var playListItem =
                 _db.LoadRecords<Song>("songs").Result.FirstOrDefault(x => x.SongTitle == playListDto.SongTitle);
        

            if (playListItem != null)
            {
                PlayList playList = new PlayList
                {
                    CreatedAt = DateTime.Now,
                    Song = playListItem.SongTitle,
                    Artist = playListItem.StageName
                };

                if (playList != null)
                    await _db.InsertRecord("playlists", playList);
            }

            return NoContent();
        }


        // DELETE /api/playlist/delete
        [HttpDelete("{id:length(36)}")]
        [Route("delete")]
        public async Task<ActionResult<PlayList>> DeleteAlbum([FromBody] PlayListDto playListDto)
        {
            if (playListDto.Id == Guid.Empty)
            {
                var apiException = new ApiException(400, "Missing Id in body");

                return BadRequest(apiException.Message);
            }

            var playList = await _db.LoadRecordById<PlayList>("playlists", playListDto.Id);

            if (playList == null)
            {
                return NotFound();
            }

            await _db.DeleteRecord<PlayList>("playlists", playListDto.Id);

            return NoContent();
        }
    }
}
