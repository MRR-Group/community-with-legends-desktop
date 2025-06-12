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

public class GameRepositoryTests
{
    [Fact]
    public async Task All_ShouldReturnEntities_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var gameDto = new GameDto
        {
            Id = 1,
            Name = "Chess",
            Cover = "https://example.com/cover.jpg"
        };
        httpTest.RespondWithJson(new Response<GameDto[]>
        {
            Data = new[] { gameDto },
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new GameRepository(new CookieSession("https://api.example.com"));
        var result = await repo.All();
        Assert.Single(result);
        Assert.Equal(gameDto.Id, result[0].Id);
    }

    [Fact]
    public async Task ById_ShouldReturnEntity_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var gameDto = new GameDto
        {
            Id = 2,
            Name = "Go",
            Cover = "https://example.com/cover2.jpg"
        };
        httpTest.RespondWithJson(new Response<GameDto>
        {
            Data = gameDto,
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new GameRepository(new CookieSession("https://api.example.com"));
        var result = await repo.ById(2);
        Assert.Equal(gameDto.Id, result.Id);
    }

    [Fact]
    public async Task ById_ShouldThrowFlurlParsingException_WhenApiReturnsInvalidData()
    {
        using var httpTest = new HttpTest();
        httpTest.RespondWith("Invalid JSON", 200, "OK", "application/json");
        var repo = new GameRepository(new CookieSession("https://api.example.com"));
        await Assert.ThrowsAsync<Flurl.Http.FlurlParsingException>(async () => await repo.ById(2));
    }
}

