namespace MT.Innovation.Shared.Utils
{
    public class PagedResult<T> where T : class
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

        public IEnumerable<T> Items { get; set; }


        /// <summary>
        /// Intialize paged result from paged list to use in serializable json Apis.
        /// </summary>
        /// <param name="pagedList"></param>
        public PagedResult(PagedList<T> pagedList)
        {
            Items = pagedList ?? throw new ArgumentNullException(nameof(pagedList));
            TotalCount = pagedList.TotalCount;
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
        }

    }
}

