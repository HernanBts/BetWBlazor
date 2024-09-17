using BetWBlazor.Backend.Repositories.Interfaces;
using BetWBlazor.Backend.UnitsOfWork.Interfaces;
using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.UnitsOfWork.Implementations;

public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
    {
        _countriesRepository = countriesRepository;
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await _countriesRepository.GetAsync();

    public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countriesRepository.GetAsync(pagination);

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _countriesRepository.GetTotalRecordsAsync(pagination);
}