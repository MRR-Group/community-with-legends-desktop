using System;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class AssetDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new AssetDto
        {
            Id = 1,
            Type = "Image",
            Link = "https://example.com/asset.jpg"
        };

        var entity = dto.ToEntity();

        Assert.NotNull(entity);
        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(AssetType.Image, entity.Type);
        Assert.Equal(dto.Link, entity.Link.ToString());
    }

    [Fact]
    public void ToEntity_ShouldReturnNull_WhenLinkIsInvalid()
    {
        var dto = new AssetDto
        {
            Id = 2,
            Type = "Image",
            Link = "not-a-valid-uri"
        };

        var entity = dto.ToEntity();
        Assert.Null(entity);
    }
}

