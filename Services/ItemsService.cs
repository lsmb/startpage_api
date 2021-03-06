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

    public async Task<Item?> GetAsync(string name) =>
        await _itemsCollection.Find(x => x.name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(Item newItem) =>
        await _itemsCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string name, Item updatedItem) =>
        await _itemsCollection.ReplaceOneAsync(x => x.name == name, updatedItem);

    public async Task RemoveAsync(string name) =>
        await _itemsCollection.DeleteOneAsync(x => x.name == name);

    public async Task RemoveCategoryAsync(string category) =>
        await _itemsCollection.DeleteManyAsync(x => x.name == category);
}
