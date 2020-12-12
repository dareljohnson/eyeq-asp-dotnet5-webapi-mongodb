using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using MongoDB.Bson.Serialization.Attributes;

namespace API.DTOs
{
    public class AlbumDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public string RecordLabel { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
