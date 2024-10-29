using cp6.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cp6.Repository
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly IMongoCollection<InventoryItem> _inventoryItems;

        public InventoryItemRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _inventoryItems = database.GetCollection<InventoryItem>("InventoryItems");
        }

        public async Task<List<InventoryItem>> GetAllAsync() => await _inventoryItems.Find(p => true).ToListAsync();

        public async Task<InventoryItem> GetByIdAsync(string id) => await _inventoryItems.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(InventoryItem inventoryItem) => await _inventoryItems.InsertOneAsync(inventoryItem);

        public async Task UpdateAsync(string id, InventoryItem inventoryItem) => await _inventoryItems.ReplaceOneAsync(p => p.Id == id, inventoryItem);

        public async Task DeleteAsync(string id) => await _inventoryItems.DeleteOneAsync(p => p.Id == id);
    }
}
