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

public class AdminRepositoryTests
{
    [Fact]
    public async Task All_ShouldReturnEntities_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var adminDto = new AdministratorDto
        {
            Id = 1,
            Name = "AdminUser",
            Email = "admin@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = new[] { "Administrator" },
            Permissions = new[] { "BanUsers" },
            CreationDate = "2025-06-11T00:00:00Z"
        };
        httpTest.RespondWithJson(new Response<AdministratorDto[]>
        {
            Data = new[] { adminDto },
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new AdminRepository(new CookieSession("https://api.example.com"));
        var result = await repo.All();
        Assert.Single(result);
        Assert.Equal(adminDto.Id, result[0].Id);
    }

    [Fact]
    public async Task ById_ShouldReturnEntity_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var adminDto = new AdministratorDto
        {
            Id = 2,
            Name = "AdminUser2",
            Email = "admin2@example.com",
            Avatar = "https://example.com/avatar2.jpg",
            IsBanned = false,
            Roles = new[] { "Administrator" },
            Permissions = new[] { "BanUsers" },
            CreationDate = "2025-06-11T00:00:00Z"
        };
        httpTest.RespondWithJson(new Response<AdministratorDto>
        {
            Data = adminDto,
            Meta = new Meta { CurrentPage = 1, LastPage = 1 }
        });
        var repo = new AdminRepository(new CookieSession("https://api.example.com"));
        var result = await repo.ById(2);
        Assert.Equal(adminDto.Id, result.Id);
    }

    [Fact]
    public async Task ById_ShouldThrowFlurlParsingException_WhenApiReturnsInvalidData()
    {
        using var httpTest = new HttpTest();
        httpTest.RespondWith("Invalid JSON", 200, "OK", "application/json");
        var repo = new AdminRepository(new CookieSession("https://api.example.com"));
        await Assert.ThrowsAsync<Flurl.Http.FlurlParsingException>(async () => await repo.ById(2));
    }
}

