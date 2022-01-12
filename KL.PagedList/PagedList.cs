using System;
using System.Collections.Generic;
using System.Linq;

namespace KL.PagedList
{
    /// <summary>
    /// Modeled after https://github.com/cornflourblue/JW.Pager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : PaginationData<T>
    {
        public PagedList(List<T> data, int totalItems, int currentPage = 1, int pageSize = 10, int maxPages = 10)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (totalItems < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalItems),
                    $"{nameof(totalItems)} cannot be less than 0");
            }

            if (currentPage < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage),
                    $"{nameof(currentPage)} cannot be less than 1");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize),
                    $"{nameof(pageSize)} cannot be less than 1");
            }

            // calculate total pages
            var totalPages = totalItems > 0
                ? (int)Math.Ceiling(totalItems / (decimal)pageSize)
                : 0;

            if (totalPages > 0 && currentPage > totalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage),
                    $"{nameof(currentPage)} cannot be greater than totalPages");
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor(maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling(maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

            // update object instance with all pager properties required by the view
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;

            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

            Data = data;
            HasPrevious = currentPage > 1;
            HasNext = currentPage < totalPages;
            IsFirstPage = currentPage == 1;
            IsLastPage = currentPage == totalPages;
        }
    }
}
