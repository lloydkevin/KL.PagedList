using System.Collections.Generic;

namespace KL.PagedList
{
    public class PaginationData<T>
    {
        public int TotalItems { get; protected set; }
        public int CurrentPage { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalPages { get; protected set; }
        public int StartPage { get; protected set; }
        public int EndPage { get; protected set; }

        public bool HasPrevious { get; protected set; }
        public bool HasNext { get; protected set; }

        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }

        public List<T> Data { get; protected set; } = new List<T>();
    }
}
