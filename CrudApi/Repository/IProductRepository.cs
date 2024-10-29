using cp6.Models;

namespace cp6.Repository
{
    public interface IInventoryItemRepository
    {
        Task<List<InventoryItem>> GetAllAsync();
        Task<InventoryItem> GetByIdAsync(string id);
        Task CreateAsync(InventoryItem inventoryItem);
        Task UpdateAsync(string id, InventoryItem inventoryItem);
        Task DeleteAsync(string id);
    }
}
