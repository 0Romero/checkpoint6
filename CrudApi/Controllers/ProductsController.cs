using cp6.Models;
using cp6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cp6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IInventoryItemRepository _repository;

        public InventoryItemsController(IInventoryItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventoryItems = await _repository.GetAllAsync();
            return Ok(inventoryItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var inventoryItem = await _repository.GetByIdAsync(id);
            if (inventoryItem == null) return NotFound();
            return Ok(inventoryItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventoryItem inventoryItem)
        {
            await _repository.CreateAsync(inventoryItem);
            return CreatedAtAction(nameof(GetById), new { id = inventoryItem.Id }, inventoryItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] InventoryItem inventoryItem)
        {
            var existingInventoryItem = await _repository.GetByIdAsync(id);
            if (existingInventoryItem == null) return NotFound();
            await _repository.UpdateAsync(id, inventoryItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var inventoryItem = await _repository.GetByIdAsync(id);
            if (inventoryItem == null) return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
