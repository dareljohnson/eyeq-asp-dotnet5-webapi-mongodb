using System;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Data
{
    public class Artist
    {
        [BsonId]
        public Guid Id { get; set; }
        public string StageName { get; set; }
        public int NumberOfTracks { get; set; }

    }
}