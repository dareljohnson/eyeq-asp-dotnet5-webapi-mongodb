using System;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Data
{
    public class PlayListDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public Album Album { get; set; }

    }
}