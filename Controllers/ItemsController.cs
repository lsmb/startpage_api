using StartpageAPI.Models;
using StartpageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace StartpageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemsService _itemsService;

    public ItemsController(ItemsService itemsService) =>
        _itemsService = itemsService;

    [HttpGet]
    public async Task<List<Item>> Get() =>
        await _itemsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Item>> Get(string id)
    {
        var item = await _itemsService.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    // [HttpPost]
    // public async Task<IActionResult> Post(Item newItem)
    // {
    //     if (!newItem.URL.StartsWith("http://") && !newItem.URL.StartsWith("https://") && !newItem.URL.StartsWith("www."))
    //     {
    //         newItem.URL = "https://" + newItem.URL;
    //     }
    //     Console.WriteLine(newItem.priority);
    //     await _itemsService.CreateAsync(newItem);

    //     return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
    // }

    // [HttpPut("{id:length(24)}")]
    // public async Task<IActionResult> Update(string id, Item updatedItem)
    // {
    //     var item = await _itemsService.GetAsync(id);

    //     if (item is null)
    //     {
    //         return NotFound();
    //     }

    //     updatedItem.Id = item.Id;

    //     await _itemsService.UpdateAsync(id, updatedItem);

    //     return NoContent();
    // }

    // [HttpDelete("{id:length(24)}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     var item = await _itemsService.GetAsync(id);

    //     if (item is null)
    //     {
    //         return NotFound();
    //     }

    //     await _itemsService.RemoveAsync(id);

    //     return NoContent();
    // }

    // [HttpDelete("{category}")]
    // public async Task<IActionResult> DeleteCategory(string category)
    // {
    //     var item = await _itemsService.GetAsync();

    //     if (item is null)
    //     {
    //         return NotFound();
    //     }

    //     await _itemsService.RemoveCategoryAsync(category);

    //     return NoContent();
    // }
}
