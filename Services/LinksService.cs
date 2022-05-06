using StartpageAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StartpageAPI.Services;

public class LinksService
{
    private readonly IMongoCollection<Link> _linksCollection;

    public LinksService(
        IOptions<StartpageDatabaseSettings> StartpageDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            StartpageDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            StartpageDatabaseSettings.Value.DatabaseName);

        _linksCollection = mongoDatabase.GetCollection<Link>(
            StartpageDatabaseSettings.Value.LinksCollectionName);
    }

    public async Task<List<Link>> GetAsync() =>
        await _linksCollection.Find(_ => true).SortByDescending(x => x.priority).ToListAsync();

    public async Task<Link?> GetAsync(string id) =>
        await _linksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Link newLink) =>
        await _linksCollection.InsertOneAsync(newLink);

    public async Task UpdateAsync(string id, Link updatedLink) =>
        await _linksCollection.ReplaceOneAsync(x => x.Id == id, updatedLink);

    public async Task RemoveAsync(string id) =>
        await _linksCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveCategoryAsync(string category) =>
        await _linksCollection.DeleteManyAsync(x => x.category == category);
}
