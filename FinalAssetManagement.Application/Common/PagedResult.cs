namespace FinalAssetManagement.Application.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = []; // Create a Blank List
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }


        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

    }
}