using BetWBlazor.Backend.Repositories.Interfaces;
using BetWBlazor.Backend.UnitsOfWork.Interfaces;
using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.UnitsOfWork.Implementations;

public class TeamsUnitOfWork : GenericUnitOfWork<Team>, ITeamsUnitOfWork
{
    private readonly ITeamsRepository _teamsRepository;

    public TeamsUnitOfWork(IGenericRepository<Team> repository, ITeamsRepository teamsRepository) : base(repository)
    {
        _teamsRepository = teamsRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Team>>> GetAsync() => await _teamsRepository.GetAsync();

    public override async Task<ActionResponse<Team>> GetAsync(int id) => await _teamsRepository.GetAsync(id);

    public async Task<IEnumerable<Team>> GetComboAsync(int countryId) => await _teamsRepository.GetComboAsync(countryId);

    public async Task<ActionResponse<Team>> AddAsync(TeamDTO model) => await _teamsRepository.AddAsync(model);

    public async Task<ActionResponse<Team>> UpdateAsync(TeamDTO model) => await _teamsRepository.UpdateAsync(model);
}