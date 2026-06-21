namespace backend.Common;

public record PagedResult<T>(
    IReadOnlyList<T> Data,
    int Total,
    int Page,
    int PageSize,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage)
{
    public static PagedResult<T> Create(
        IReadOnlyList<T> data,
        int total,
        int page,
        int pageSize)
    {
        page = page < 1 ? 1 : page;
        var totalPages = pageSize > 0 ? (int)Math.Ceiling(total / (double)pageSize) : 0;

        return new PagedResult<T>(
            data,
            total,
            page,
            pageSize,
            totalPages,
            page < totalPages,
            page > 1);
    }
}
