using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace API.DTOs
{
    public class SongDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public string SongTitle { get; set; }
        public string StageName { get; set; }
        public double Duration { get; set; }

    }
}
