using BetWBlazor.Backend.Helpers;
using BetWBlazor.Share.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BetWBlazor.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;

    public SeedDb(DataContext context, IFileStorage fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
        await CheckTeamsAsync();
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            var sqlScript = File.ReadAllText("Data\\Countries.sql");
            await _context.Database.ExecuteSqlRawAsync(sqlScript);
        }
    }

    private async Task CheckTeamsAsync()
    {
        if (!_context.Teams.Any())
        {
            foreach (var country in _context.Countries)
            {
                var imagePath = string.Empty;
                var filePath = $"{Environment.CurrentDirectory}\\Images\\Flags\\{country.Name}.png";
                if (File.Exists(filePath))
                {
                    var fileBytes = File.ReadAllBytes(filePath);
                    imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "flags");
                }
                _context.Teams.Add(new Team { Name = country.Name, Country = country!, Image = imagePath });
                if (country.Name == "Argentina")
                {
                    _context.Teams.Add(new Team { Name = "BOCA", Country = country! });
                    _context.Teams.Add(new Team { Name = "River", Country = country! });
                    _context.Teams.Add(new Team { Name = "Racing", Country = country! });
                    _context.Teams.Add(new Team { Name = "Independiente", Country = country! });
                    _context.Teams.Add(new Team { Name = "San Lorenzo", Country = country! });
                }
                if (country.Name == "Colombia")
                {
                    _context.Teams.Add(new Team { Name = "Medellín", Country = country! });
                    _context.Teams.Add(new Team { Name = "Nacional", Country = country! });
                    _context.Teams.Add(new Team { Name = "Millonarios", Country = country! });
                    _context.Teams.Add(new Team { Name = "Junior", Country = country! });
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}