using System;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Data
{
    public class PlayList
    {
        [BsonId]
        public Guid Id { get; set; }
        public Album Album { get; set; }
        public string Song { get; set; }
        public string Artist { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}