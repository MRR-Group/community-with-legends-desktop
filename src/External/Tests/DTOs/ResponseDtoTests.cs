using System;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class ResponseDtoTests
{
    [Fact]
    public void Meta_ShouldMapPropertiesCorrectly()
    {
        var meta = new Meta
        {
            CurrentPage = 2,
            LastPage = 5
        };
        Assert.Equal(2, meta.CurrentPage);
        Assert.Equal(5, meta.LastPage);
    }

    [Fact]
    public void Response_ShouldMapPropertiesCorrectly()
    {
        var meta = new Meta { CurrentPage = 1, LastPage = 3 };
        var response = new Response<string> { Data = "test", Meta = meta };
        Assert.Equal("test", response.Data);
        Assert.Equal(meta, response.Meta);
    }
}

