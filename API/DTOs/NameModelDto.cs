using System;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Data
{
    public class NameModelDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}