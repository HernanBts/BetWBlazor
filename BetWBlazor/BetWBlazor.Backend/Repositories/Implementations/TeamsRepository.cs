﻿using BetWBlazor.Backend.Data;
using BetWBlazor.Backend.Helpers;
using BetWBlazor.Backend.Repositories.Interfaces;
using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;
using Microsoft.EntityFrameworkCore;

namespace BetWBlazor.Backend.Repositories.Implementations;

public class TeamsRepository : GenericRepository<Team>, ITeamsRepository
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;

    public TeamsRepository(DataContext context, IFileStorage fileStorage) : base(context)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public override async Task<ActionResponse<IEnumerable<Team>>> GetAsync()
    {
        var teams = await _context.Teams
            .Include(x => x.Country)
            .OrderBy(x => x.Name)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Team>>
        {
            WasSuccess = true,
            Result = teams
        };
    }

    public override async Task<ActionResponse<Team>> GetAsync(int id)
    {
        var team = await _context.Teams
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (team is null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }
        return new ActionResponse<Team>
        {
            WasSuccess = true,
            Result = team
        };
    }

    public async Task<ActionResponse<Team>> AddAsync(TeamDTO teamDTO)
    {
        var country = await _context.Countries.FindAsync(teamDTO.CountryId);
        if (country is null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR04"
            };
        }

        var team = new Team
        {
            Country = country,
            Name = teamDTO.Name,
        };

        if (!string.IsNullOrEmpty(teamDTO.Image))
        {
            var imageBase64 = Convert.FromBase64String(teamDTO.Image);
            team.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        }

        _context.Add(team);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Team>
            {
                WasSuccess = true,
                Result = team
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<IEnumerable<Team>> GetComboAsync(int countryId)
    {
        return await _context.Teams
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO)
    {
        var currentTeam = await _context.Teams.FindAsync(teamDTO.Id);
        if (currentTeam is null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR05"
            };
        }

        var country = await _context.Teams.FindAsync(teamDTO.CountryId);
        if (country is null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR04"
            };
        }

        if (!string.IsNullOrEmpty(teamDTO.Image))
        {
            var imageBase64 = Convert.FromBase64String(teamDTO.Image);
            currentTeam.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        }

        currentTeam.Country = country;
        currentTeam.Name = teamDTO.Name;

        _context.Update(currentTeam);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Team>
            {
                WasSuccess = true,
                Result = currentTeam
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = ex.Message,
            };
        }
    }
}