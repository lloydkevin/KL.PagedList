using System.Linq;
using FluentAssertions;
using Xunit;

namespace KL.PagedList.Tests
{
    public class PagedListSimpleTests
    {
        [Theory]
        [InlineData(1, true, false)]
        [InlineData(2, false, false)]
        [InlineData(9, false, false)]
        [InlineData(10, false, true)]
        public void IsFirstOrLastPage(int pageNumber, bool isFirstPage, bool isLastPage)
        {
            var list = Enumerable.Range(1, 10).ToList();

            var result = list.ToPagedList(100, pageNumber, pageSize: 10);
            result.IsFirstPage.Should().Be(isFirstPage);
            result.IsLastPage.Should().Be(isLastPage);
        }

        [Theory]
        [InlineData(1, true, false)]
        [InlineData(2, true, true)]
        [InlineData(3, false, true)]
        public void TraversePagesWith_HasNextAndHasPrevious(int pageNumber, bool hasNext, bool hasPrevious)
        {
            var list = Enumerable.Range(1, 10).ToList();

            var result = list.ToPagedList(30, pageNumber, pageSize: 10);
            result.Data.Should().BeEquivalentTo(list);
            result.CurrentPage.Should().Be(pageNumber);
            result.TotalItems.Should().Be(30);
            result.TotalPages.Should().Be(3);
            result.HasNext.Should().Be(hasNext);
            result.HasPrevious.Should().Be(hasPrevious);
            result.StartPage.Should().Be(1);
            result.EndPage.Should().Be(3);
        }

        [Theory]
        [InlineData(1, 1, 10)]
        [InlineData(2, 1, 10)]
        [InlineData(3, 1, 10)]
        [InlineData(4, 1, 10)]
        [InlineData(5, 1, 10)]
        [InlineData(6, 1, 10)]

        [InlineData(7, 2, 11)]
        [InlineData(8, 3, 12)]
        [InlineData(9, 4, 13)]
        [InlineData(10, 5, 14)]

        [InlineData(11, 6, 15)]
        [InlineData(12, 6, 15)]
        [InlineData(13, 6, 15)]
        [InlineData(14, 6, 15)]
        [InlineData(15, 6, 15)]
        public void TraverseWith_StartAndEndPages(int pageNumber, int startPage, int endPage)
        {
            var list = Enumerable.Range(1, 10).ToList();

            var result = list.ToPagedList(150, pageNumber, pageSize: 10, maxPages: 10);
            result.StartPage.Should().Be(startPage);
            result.EndPage.Should().Be(endPage);
        }

        [Theory]
        [InlineData(1, 1, 10)]
        [InlineData(2, 11, 20)]
        [InlineData(3, 21, 30)]
        [InlineData(4, 31, 40)]
        [InlineData(5, 41, 50)]
        [InlineData(6, 51, 60)]
        [InlineData(7, 61, 70)]
        [InlineData(8, 71, 80)]
        [InlineData(9, 81, 90)]
        [InlineData(10, 91, 100)]
        [InlineData(15, 141, 150)]
        [InlineData(16, 151, 151)]
        public void Queryable(int pageNumber, int firstResult, int lastResult)
        {
            var list = Enumerable.Range(1, 151).ToList();
            var source = list.AsQueryable();

            var result = source.ToPagedList(pageNumber, pageSize: 10);
            result.Data.Should().HaveCount(x => x <= 10);
            result.TotalItems.Should().Be(list.Count);
            result.TotalPages.Should().Be(16);
            result.Data.First().Should().Be(firstResult);
            result.Data.Last().Should().Be(lastResult);
            result.CurrentPage.Should().Be(pageNumber);
        }
    }
}
