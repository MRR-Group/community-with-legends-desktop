using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class CommentDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Commenter",
            Email = "commenter@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var dto = new CommentDto
        {
            Id = 10,
            Content = "Nice post!",
            CreationDate = "2025-06-11T00:00:00Z",
            User = userDto
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Content, entity.Content);
        Assert.Equal("2025.06.11 00:00", entity.CreationDate.ToString());
        Assert.Equal(dto.User.Id, entity.User.Id);
    }
}


