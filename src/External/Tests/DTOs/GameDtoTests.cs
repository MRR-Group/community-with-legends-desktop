using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class GameDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new GameDto
        {
            Id = 1,
            Name = "Chess",
            Cover = "https://example.com/cover.jpg"
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Name, entity.Name);
        Assert.Equal(dto.Cover, entity.Cover.ToString());
    }
}

