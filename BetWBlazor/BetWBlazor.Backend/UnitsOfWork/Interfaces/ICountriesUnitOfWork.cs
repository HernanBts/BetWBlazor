using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.UnitsOfWork.Interfaces;

public interface ICountriesUnitOfWork
{
    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();

    Task<IEnumerable<Country>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}