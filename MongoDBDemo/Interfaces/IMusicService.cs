using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IMusicService
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        Task InsertRecord<T>(string table, T record);
        Task<List<T>> LoadRecords<T>(string table);
        Task<T> LoadRecordById<T>(string table, Guid id);
        Task<T> LoadRecordBySongTitle<T>(string table, string title);
        Task<T> LoadRecordByAlbumTitle<T>(string table, string title);
        Task<T> LoadRecordByArtistTitle<T>(string table, string title);
        Task UpsertRecord<T>(string table, Guid id, T record);
        Task DeleteRecord<T>(string table, Guid id);

    }
}