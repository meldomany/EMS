namespace EMS.Shared.DTOs
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        private const int MaxPageSize = 100;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
