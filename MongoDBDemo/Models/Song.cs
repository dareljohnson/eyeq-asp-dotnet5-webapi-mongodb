using System;
using System.Collections;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Data
{
    public class Song
    {
        [BsonId]
        public Guid Id { get; set; }
        public string SongTitle { get; set; }
        public string StageName { get; set; }
        public double Duration { get; set; }

    }
}