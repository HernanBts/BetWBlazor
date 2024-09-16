using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.UnitsOfWork.Interfaces;

public interface ITeamsUnitOfWork
{
    Task<ActionResponse<Team>> AddAsync(TeamDTO TeamDTO);

    Task<ActionResponse<Team>> UpdateAsync(TeamDTO TeamDTO);

    Task<ActionResponse<Team>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Team>>> GetAsync();

    Task<IEnumerable<Team>> GetComboAsync(int countryId);
}