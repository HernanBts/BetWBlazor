using BetWBlazor.Backend.Data;
using BetWBlazor.Backend.Repositories.Interfaces;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;
using Microsoft.EntityFrameworkCore;

namespace BetWBlazor.Backend.Repositories.Implementations;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;

    public CountriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        var result = await _context.Countries
            .Include(x => x.Teams)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            return new ActionResponse<Country>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = result
        };
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var result = await _context.Countries
            .Include(x => x.Teams)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = result
        };
    }

    public async Task<IEnumerable<Country>> GetComboAsync()
    {
        return await _context.Countries.OrderBy(x => x.Name).ToListAsync();
    }
}