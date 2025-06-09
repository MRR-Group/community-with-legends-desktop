using Application.Abstractions;
using Flurl.Http;
using Infrastructure.DTOs;

namespace Infrastructure.Services;


public class GameService : IGameService
{
    private CookieSession _session;

    public GameService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<int> Progress()
    {
        var response = await _session.Request("games/import/progress")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetJsonAsync<FetchingProgress>();
        
        return response.Progress;
    }
    
    public async Task<int> Fetch()
    {
        await _session.Request("games/import")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .PostAsync();

        return 0;
    }
}
