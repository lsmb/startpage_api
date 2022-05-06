using StartpageAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StartpageAPI.Services;

public class ItemsService
{
    private readonly IMongoCollection<Item> _itemsCollection;

    public ItemsService(
        IOptions<StartpageDatabaseSettings> StartpageDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            StartpageDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            StartpageDatabaseSettings.Value.DatabaseName);

        _itemsCollection = mongoDatabase.GetCollection<Item>(
            StartpageDatabaseSettings.Value.ItemsCollectionName);
    }

    public async Task<List<Item>> GetAsync() =>
        await _itemsCollection.Find(_ => true).SortByDescending(x => x.priority).ToListAsync();

    public async Task<Item?> GetAsync(string id) =>
        await _itemsCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Item newItem) =>
        await _itemsCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string id, Item updatedItem) =>
        await _itemsCollection.ReplaceOneAsync(x => x.id == id, updatedItem);

    public async Task RemoveAsync(string id) =>
        await _itemsCollection.DeleteOneAsync(x => x.id == id);

    public async Task RemoveCategoryAsync(string category) =>
        await _itemsCollection.DeleteManyAsync(x => x.name == category);
}
