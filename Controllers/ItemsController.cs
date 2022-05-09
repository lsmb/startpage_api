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


    [HttpPost]
    public async Task<IActionResult> Post(Item newItem)
    {

        Console.WriteLine(newItem);
        await _itemsService.CreateAsync(newItem);

        return CreatedAtAction(nameof(Get), new { id = newItem.id }, newItem);
    }

    [HttpPost("{name}")]
    public async Task<IActionResult> PostLink(string name, Link newLink)
    {
        if (!newLink.URL.StartsWith("http://") && !newLink.URL.StartsWith("https://") && !newLink.URL.StartsWith("www."))
        {
            newLink.URL = "https://" + newLink.URL;
        }
        Console.WriteLine(newLink.priority);
        var item = await _itemsService.GetAsync(name);
        if (item is null)
        {
            return NotFound();
        }

        if (item.links is null)
        {
            item.links = new List<Link>();
        }

        item.links.Add(newLink);

        if (item.id is null)
        {
            return NotFound();
        }

        await _itemsService.UpdateAsync(item.id, item);


        // await _itemsService.CreateAsync(newLink);

        return NoContent();
    }


    // [HttpPost("name")]
    // public async Task<IActionResult> PostLink(string name, Link newLink)
    // {
    //     if (!newLink.URL.StartsWith("http://") && !newLink.URL.StartsWith("https://") && !newLink.URL.StartsWith("www."))
    //     {
    //         newLink.URL = "https://" + newLink.URL;
    //     }
    //     Console.WriteLine(newLink.priority);
    //     var item = await _itemsService.GetAsync(name);
    //     if (item is null)
    //     {
    //         return NotFound();
    //     }

    //     if (item.links is null)
    //     {
    //         item.links = new List<Link>();
    //     }

    //     item.links.Add(newLink);

    //     if (item.id is null)
    //     {
    //         return NotFound();
    //     }

    //     await _itemsService.UpdateAsync(item.id, item);


    //     // await _itemsService.CreateAsync(newLink);

    //     return NoContent();
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

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var item = await _itemsService.GetAsync(name);

        if (item is null)
        {
            return NotFound();
        }

        await _itemsService.RemoveAsync(name);

        return NoContent();
    }

    [HttpDelete("{itemName}/{linkName}")]
    public async Task<IActionResult> DeleteLink(string itemName, string linkName)
    {
        var item = await _itemsService.GetAsync(itemName);

        if (item is null || item.id is null)
        {
            return NotFound();
        }

        if (item.links is not null && item.links.Count > 0)
        {
            item.links.RemoveAll(link => link.name == linkName);
        }
        else
        {
            return NotFound();
        }

        await _itemsService.UpdateAsync(item.id, item);

        return NoContent();
    }
}
