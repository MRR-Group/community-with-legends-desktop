using System;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class UserDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new UserDto
        {
            Id = 1,
            Name = "TestUser",
            Email = "test@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = true,
            Roles = ["Moderator", "User"],
            Permissions = ["AnonymizeUsers", "BanUsers"],
            CreationDate = "2025-06-11T00:00:00Z"
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Name, entity.Name);
        Assert.Equal(dto.Email, entity.Email.Value);
        Assert.Equal(dto.Avatar, entity.Avatar.ToString());
        Assert.Equal(dto.IsBanned, entity.IsBanned);
        Assert.Equal("2025.06.11 00:00", entity.CreationDate.ToString());
        Assert.Contains(Role.Moderator, entity.Roles.Value);
        Assert.Contains(Role.User, entity.Roles.Value);
        Assert.Contains(Permission.BanUsers, entity.Permissions.Value);
        Assert.Contains(Permission.AnonymizeUsers, entity.Permissions.Value);
    }

    [Fact]
    public void ToEntity_ShouldHandleEmptyRolesAndPermissions()
    {
        var dto = new UserDto
        {
            Id = 2,
            Name = "NoRoles",
            Email = "noroles@example.com",
            Avatar = "https://example.com/avatar2.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };

        var entity = dto.ToEntity();
        
        Assert.Empty(entity.Roles.Value);
        Assert.Empty(entity.Permissions.Value);
    }
}

