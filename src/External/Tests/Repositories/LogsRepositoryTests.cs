using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Flurl.Http;
using Flurl.Http.Testing;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Xunit;

namespace Tests.Repositories;

public class LogsRepositoryTests
{
    [Fact]
    public async Task All_ShouldReturnEntities_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Logger",
            Email = "logger@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = new[] { "User" },
            Permissions = new[] { "BanUsers" },
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var logDto = new LogDto
        {
            Id = 1,
            Description = "Test log entry",
            CreationDate = "2025-06-11T00:00:00Z",
            Causer = userDto
        };
        httpTest.RespondWithJson(new Response<LogDto[]>
        {
            Data = new[] { logDto },
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new LogsRepository(new CookieSession("https://api.example.com"));
        var result = await repo.All();
        Assert.Single(result);
        Assert.Equal(logDto.Id, result[0].Id);
    }

    [Fact]
    public async Task ById_ShouldReturnEntity_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var userDto = new UserDto
        {
            Id = 2,
            Name = "Logger2",
            Email = "logger2@example.com",
            Avatar = "https://example.com/avatar2.jpg",
            IsBanned = false,
            Roles = new[] { "User" },
            Permissions = new[] { "BanUsers" },
            CreationDate = "2025-06-11T00:00:00Z"
        };
        var logDto = new LogDto
        {
            Id = 2,
            Description = "Test log entry 2",
            CreationDate = "2025-06-11T00:00:00Z",
            Causer = userDto
        };
        httpTest.RespondWithJson(new Response<LogDto>
        {
            Data = logDto,
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new LogsRepository(new CookieSession("https://api.example.com"));
        var result = await repo.ById(2);
        Assert.Equal(logDto.Id, result.Id);
    }

    [Fact]
    public async Task ById_ShouldThrowFlurlParsingException_WhenApiReturnsInvalidData()
    {
        using var httpTest = new HttpTest();
        httpTest.RespondWith("Invalid JSON", 200, "OK", "application/json");
        var repo = new LogsRepository(new CookieSession("https://api.example.com"));
        await Assert.ThrowsAsync<Flurl.Http.FlurlParsingException>(async () => await repo.ById(2));
    }
}

