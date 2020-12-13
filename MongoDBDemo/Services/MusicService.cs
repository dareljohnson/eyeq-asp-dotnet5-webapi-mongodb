using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Data
{
    public class MusicService : IMusicService
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        private IMongoDatabase db;

        public MusicService(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public MusicService(IMusicDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            db = client.GetDatabase(settings.DatabaseName);
        }

        public async Task InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }

        public async Task<List<T>> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return await collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<T> LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<T> LoadRecordBySongTitle<T>(string table, string title)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Song", title);

            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<T> LoadRecordByAlbumTitle<T>(string table, string title)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("albums", title);

            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<T> LoadRecordByArtistTitle<T>(string table, string title)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("artists", title);

            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            var result = await collection
                .FindOneAndReplaceAsync(
                    filter,
                    record,
                    new FindOneAndReplaceOptions<T> { IsUpsert = true });
        }

        public async Task DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
        }

    }
}