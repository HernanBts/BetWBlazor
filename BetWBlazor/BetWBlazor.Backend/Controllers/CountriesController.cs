using BetWBlazor.Backend.Data;
using BetWBlazor.Share.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetWBlazor.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Countries.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country is null)
            return NotFound();

        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        if (country is null)
            return BadRequest();

        _context.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(Country country)
    {
        if (country is null)
            return BadRequest();

        var result = await _context.Countries.FindAsync(country.Id);

        if (result is null)
            return NotFound();

        result.Name = country.Name;
        _context.Update(result);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);

        if (country is null)
            return NotFound();

        _context.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}