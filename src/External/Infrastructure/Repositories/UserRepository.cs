using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class UserRepository
{
    private CookieSession _session;

    public UserRepository(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<User> ById(uint id)
    {
        var response = await _session.Request("users")
            .AppendPathSegment(id)
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<UserDto>>();

        return json.Data.ToEntity();
    }
    
    public async Task<User[]> All()
    {
        var response = await _session.Request("users")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<UserDto[]>>();

        return json.Data.Select(dto => dto.ToEntity()).ToArray();
    }
}
