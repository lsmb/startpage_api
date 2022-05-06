using StartpageAPI.Models;
using StartpageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace StartpageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BgController : ControllerBase
{
    private readonly BgService _bgService;

    public BgController(BgService bgService) =>
        _bgService = bgService;

    // [HttpGet]
    // public async Task<Bg> Get() =>
    //     await _bgService.GetAsync();

    [HttpGet]
    public async Task<List<Bg>> Get() =>
        await _bgService.GetAsync();


    [HttpPost]
    public async Task<IActionResult> Post(Bg newBg)
    {
        Console.WriteLine("Added background:", newBg.URL);
        await _bgService.CreateAsync(newBg);

        return CreatedAtAction(nameof(Get), new { id = newBg.Id }, newBg);
    }

    // [HttpPut("{id:length(24)}")]
    // public async Task<IActionResult> Update(string id, Link updatedLink)
    // {
    //     var link = await _bgService.GetAsync(id);

    //     if (link is null)
    //     {
    //         return NotFound();
    //     }

    //     updatedLink.Id = link.Id;

    //     await _bgService.UpdateAsync(id, updatedLink);

    //     return NoContent();
    // }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var link = await _bgService.GetAsync(id);

        if (link is null)
        {
            return NotFound();
        }

        await _bgService.RemoveAsync(id);

        return NoContent();
    }
}
