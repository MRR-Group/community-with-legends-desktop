using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class StatisticsRepository
{
    private CookieSession _session;

    public StatisticsRepository(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<Statistics> Get()
    {
        var response = await _session.Request("statistics")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetJsonAsync<Statistics>();

        return response;
    }
}
