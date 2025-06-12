using System;
using Domain.Entities;
using Infrastructure.DTOs;
using Xunit;

namespace Tests.DTOs;

public class PostDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Poster",
            Email = "poster@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = [],
            Permissions = [],
            CreationDate = "2025-06-11T00:00:00Z"
        };
        
        var tagDto = new TagDto { Id = 2, Name = "Strategy" };
        var assetDto = new AssetDto { Id = 3, Type = "Image", Link = "https://example.com/asset.jpg" };
        var commentDto = new CommentDto
        {
            Id = 4,
            Content = "Great post!",
            CreationDate = "2025-06-11T00:00:00Z",
            User = userDto
        };
        
        var gameDto = new GameDto { Id = 5, Name = "Chess", Cover = "https://example.com/cover.jpg" };
        var dto = new PostDto
        {
            Id = 10,
            Content = "Post content",
            CreationDate = "2025-06-11T00:00:00Z",
            User = userDto,
            Game = gameDto,
            Tags = new[] { tagDto },
            Asset = assetDto,
            Reactions = 7,
            Comments = new[] { commentDto }
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Content, entity.Content);
        Assert.Equal("2025.06.11 00:00", entity.CreationDate.ToString());
        Assert.Equal(dto.User.Id, entity.User.Id);
        Assert.Equal(dto.Game.Id, entity.Game.Id);
        Assert.Single(entity.Tags);
        Assert.Equal(dto.Tags[0].Id, entity.Tags[0].Id);
        Assert.Equal(dto.Asset.Id, entity.Asset.Id);
        Assert.Equal(dto.Reactions, entity.Reactions);
        Assert.Single(entity.Comments);
        Assert.Equal(dto.Comments[0].Id, entity.Comments[0].Id);
    }
}

