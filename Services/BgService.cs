using StartpageAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StartpageAPI.Services;

public class BgService
{
    private readonly IMongoCollection<Bg> _bgCollection;

    public BgService(
        IOptions<StartpageDatabaseSettings> StartpageDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            StartpageDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            StartpageDatabaseSettings.Value.DatabaseName);

        _bgCollection = mongoDatabase.GetCollection<Bg>(
            StartpageDatabaseSettings.Value.BgCollectionName);
    }

    public async Task<List<Bg>> GetAsync() =>
        await _bgCollection.Find(_ => true).ToListAsync();

    public async Task<Bg?> GetAsync(string id) =>
        await _bgCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    // public async Task<Bg> GetAsync() =>
    //     await _bgCollection.Find(_ => true).SortByDescending(x => x.Id).FirstOrDefaultAsync();

    // public async Task<Bg?> GetAsync(string id) =>
    //     await _bgCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Bg newBg) =>
        await _bgCollection.InsertOneAsync(newBg);

    // public async Task UpdateAsync(string id, Bg updatedBg) =>
    //     await _bgCollection.ReplaceOneAsync(x => x.Id == id, updatedBg);

    public async Task RemoveAsync(string id) =>
        await _bgCollection.DeleteOneAsync(x => x.Id == id);

    // public async Task RemoveCategoryAsync(string category) =>
    //     await _bgCollection.DeleteManyAsync(x => x.category == category);
}
