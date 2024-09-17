using BetWBlazor.Share.DTOs;

namespace BetWBlazor.Backend.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queriable, PaginationDTO pagination)
    {
        return queriable
            .Skip((pagination.Page - 1) * pagination.RecordsNumber)
            .Take(pagination.RecordsNumber);
    }
}