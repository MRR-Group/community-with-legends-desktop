using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class LogDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Logger",
            Email = "logger@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var dto = new LogDto
        {
            Id = 5,
            Description = "Test log entry",
            CreationDate = "2025-06-11T00:00:00Z",
            Causer = userDto
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Description, entity.Description);
        Assert.Equal("2025.06.11 00:00", entity.CreationDate.ToString());
        Assert.Equal(dto.Causer.Id, entity.Causer.Id);
    }
}

