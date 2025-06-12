using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class TagDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new TagDto
        {
            Id = 1,
            Name = "Action"
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Name, entity.Name);
    }
}

