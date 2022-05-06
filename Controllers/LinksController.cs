using StartpageAPI.Models;
using StartpageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace StartpageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly LinksService _linksService;

    public LinksController(LinksService linksService) =>
        _linksService = linksService;

    [HttpGet]
    public async Task<List<Link>> Get() =>
        await _linksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Link>> Get(string id)
    {
        var link = await _linksService.GetAsync(id);

        if (link is null)
        {
            return NotFound();
        }

        return link;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Link newLink)
    {
        if (!newLink.URL.StartsWith("http://") && !newLink.URL.StartsWith("https://") && !newLink.URL.StartsWith("www."))
        {
            newLink.URL = "https://" + newLink.URL;
        }
        Console.WriteLine(newLink.priority);
        await _linksService.CreateAsync(newLink);

        return CreatedAtAction(nameof(Get), new { id = newLink.Id }, newLink);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Link updatedLink)
    {
        var link = await _linksService.GetAsync(id);

        if (link is null)
        {
            return NotFound();
        }

        updatedLink.Id = link.Id;

        await _linksService.UpdateAsync(id, updatedLink);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var link = await _linksService.GetAsync(id);

        if (link is null)
        {
            return NotFound();
        }

        await _linksService.RemoveAsync(id);

        return NoContent();
    }

    [HttpDelete("{category}")]
    public async Task<IActionResult> DeleteCategory(string category)
    {
        var link = await _linksService.GetAsync();

        if (link is null)
        {
            return NotFound();
        }

        await _linksService.RemoveCategoryAsync(category);

        return NoContent();
    }
}
