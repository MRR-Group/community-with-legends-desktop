using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class GameRepository
{
    private CookieSession _session;

    public GameRepository(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<Game[]> All()
    {
        var response = await _session.Request("games")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<GameDto[]>>();

        return json.Data.Select((dto) => dto.ToEntity()).ToArray();
    }
}
