using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.Repositories.Interfaces;

public interface ITeamsRepository
{
    Task<IEnumerable<Team>> GetComboAsync(int countryId);

    Task<ActionResponse<Team>> AddAsync(TeamDTO teamDTO);

    Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO);

    Task<ActionResponse<Team>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Team>>> GetAsync();

    Task<ActionResponse<IEnumerable<Team>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}