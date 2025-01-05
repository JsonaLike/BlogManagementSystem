namespace MT.Innovation.Shared.Utils
{
    public class PagedList<T> : List<T> where T : class
    {

        /// <summary>
        /// Gets total number of items (useful when paging is used, otherwise 0)
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets current page nubmer used to get items (useful when paging is used)
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Get page size used to get items (useful when paging is used)
        /// </summary>
        public int PageSize { get; private set; }


        public PagedList() : base()
        {
            TotalCount = 0;
            PageNumber = 0;
            PageSize = 0;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalCount">The total count (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageNumber">The page number (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageSize">The page size (if paging is used, otherwise <c>0</c>).</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize) : base(items)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

    }
}

