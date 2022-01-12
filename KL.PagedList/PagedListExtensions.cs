using System.Collections.Generic;
using System.Linq;

namespace KL.PagedList
{
    public static class PagedListExtensions
    {
        public static PaginationData<T> ToPagedList<T>(this List<T> data, int totalItems, int currentPage = 1, int pageSize = 10, int maxPages = 10)
        {
            return new PagedList<T>(data, totalItems, currentPage, pageSize, maxPages);
        }

        public static PaginationData<T> ToPagedList<T>(this IQueryable<T> source, int currentPage = 1, int pageSize = 10, int maxPages = 10)
        {
            var count = source.Count();
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return ToPagedList(items, count, currentPage, pageSize, maxPages);
        }
    }
}
