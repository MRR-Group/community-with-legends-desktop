using System.Threading.Tasks;
using Domain.Entities;
using Flurl.Http;
using Flurl.Http.Testing;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Xunit;

namespace Tests.Repositories;

public class StatisticsRepositoryTests
{
    [Fact]
    public async Task Get_ShouldReturnStatistics_WhenApiReturnsData()
    {
        using var httpTest = new HttpTest();
        var expected = new Statistics();
        
        httpTest.RespondWithJson(expected);
        
        var repo = new StatisticsRepository(new CookieSession("https://api.example.com"));
        var result = await repo.Get();
        
        Assert.NotNull(result);
    }
}

