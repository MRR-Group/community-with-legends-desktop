using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class AdminRepository
{
    private CookieSession _session;

    public AdminRepository(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<Administrator[]> All()
    {
        var response = await _session.Request("admins")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<AdministratorDto[]>>();

        return json.Data.Select(dto => dto.ToEntity()).ToArray();
    }
}
