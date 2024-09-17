using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Responses;

namespace BetWBlazor.Backend.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);

    Task<ActionResponse<int>> GetTotalRecordsAsync();

    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);
}