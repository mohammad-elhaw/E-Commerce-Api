namespace Shared
{
    public record PaginatedResult<T>(int pageIndex, int pageSize, int totalCount, int totalPages, IEnumerable<T> items);
  
}
