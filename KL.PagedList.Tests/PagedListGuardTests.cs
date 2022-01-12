using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace KL.PagedList.Tests;

public class PagedListGuardTests
{
    [Fact]
    public void List_CannotBeNull()
    {
        FluentActions.Invoking(() => new PagedList<int>(null, 100))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void TotalItems_CannotBeLessThanZero()
    {
        FluentActions.Invoking(() => new PagedList<int>(new List<int>(), -1))
            .Should().Throw<ArgumentOutOfRangeException>().WithMessage("*less than 0*");
    }

    [Fact]
    public void CurrentPage_CannotBeLessThanOne()
    {
        FluentActions.Invoking(() => new PagedList<int>(new List<int>(), 10, currentPage: 0))
            .Should().Throw<ArgumentOutOfRangeException>().WithMessage("*less than 1*");
    }

    [Fact]
    public void CurrentPage_CannotBeGreaterThanTotal()
    {
        FluentActions.Invoking(() => new PagedList<int>(new List<int>() { 1,2,3 }, 10, currentPage: 3))
            .Should().Throw<ArgumentOutOfRangeException>().WithMessage("*greater than totalPages*");
    }

    [Fact]
    public void PageSize_CannotBeLessThanOne()
    {
        FluentActions.Invoking(() => new PagedList<int>(new List<int>(), 10, pageSize: 0))
            .Should().Throw<ArgumentOutOfRangeException>().WithMessage("*less than 1*");
    }

    [Fact]
    public void EmptyList_ShouldReturn()
    {
        var result = new PagedList<int>(new List<int>(), 0);
        result.Data.Should().BeEmpty();
        result.CurrentPage.Should().Be(1);
        result.HasNext.Should().BeFalse();
        result.HasPrevious.Should().BeFalse();
    }


}
