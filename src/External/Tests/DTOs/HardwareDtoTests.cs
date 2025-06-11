using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class HardwareDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new HardwareDto
        {
            Id = 1,
            Title = "CPU",
            Value = "Intel i7"
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Title, entity.Title);
        Assert.Equal(dto.Value, entity.Value);
    }
}

