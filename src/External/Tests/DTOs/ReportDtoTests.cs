using System.Text.Json;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class ReportDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly_ForComment()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Reporter",
            Email = "reporter@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var commentDto = new CommentDto
        {
            Id = 2,
            Content = "Reported comment!",
            CreationDate = "2025-06-11T00:00:00Z",
            User = userDto
        };
        var dto = new ReportDto
        {
            Id = 10,
            Reason = "Spam",
            ReportableType = "Comment",
            ReportedAt = "2025-06-11T00:00:00Z",
            ResolvedAt = null,
            Status = ["Pending"],
            ReportedBy = userDto,
            Reportable = JsonSerializer.SerializeToElement(commentDto)
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Reason, entity.Reason);
        Assert.Equal("2025.06.11 00:00", entity.ReportedAt.ToString());
        Assert.Equal(dto.ReportedBy.Id, entity.ReportedBy.Id);
        Assert.Equal(ReportStatus.Pending, entity.Status[0]);
        Assert.IsType<Comment>(entity.Reportable);
    }

    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly_ForUserProfile()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Profiled",
            Email = "profiled@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var dto = new ReportDto
        {
            Id = 11,
            Reason = "Abuse",
            ReportableType = "User",
            ReportedAt = "2025-06-11T00:00:00Z",
            ResolvedAt = "2025-06-12T00:00:00Z",
            Status = ["Resolved"],
            ReportedBy = userDto,
            Reportable = JsonSerializer.SerializeToElement(userDto)
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Reason, entity.Reason);
        Assert.Equal("2025.06.11 00:00", entity.ReportedAt.ToString());
        Assert.Equal("2025.06.12 00:00", entity.ResolvedAt?.ToString());
        Assert.Equal(dto.ReportedBy.Id, entity.ReportedBy.Id);
        Assert.Equal(ReportStatus.Resolved, entity.Status[0]);
        Assert.IsType<UserProfile>(entity.Reportable);
    }

    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly_ForPost()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Reporter",
            Email = "reporter@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var postDto = new PostDto
        {
            Id = 3,
            Content = "Reported post!",
            CreationDate = "2025-06-11T00:00:00Z",
            User = userDto,
            Game = null,
            Tags = [],
            Asset = null,
            Reactions = 0,
            Comments = [],
        };
        var dto = new ReportDto
        {
            Id = 12,
            Reason = "Offensive content",
            ReportableType = "Post",
            ReportedAt = "2025-06-11T00:00:00Z",
            ResolvedAt = null,
            Status = new[] { "Pending" },
            ReportedBy = userDto,
            Reportable = System.Text.Json.JsonSerializer.SerializeToElement(postDto)
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Reason, entity.Reason);
        Assert.Equal("2025.06.11 00:00", entity.ReportedAt.ToString());
        Assert.Equal(dto.ReportedBy.Id, entity.ReportedBy.Id);
        Assert.Equal(ReportStatus.Pending, entity.Status[0]);
        Assert.IsType<Post>(entity.Reportable);
    }
}
