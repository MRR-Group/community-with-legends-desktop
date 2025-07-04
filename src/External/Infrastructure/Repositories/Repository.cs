using Application.Abstractions;
using Domain.Primitives;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class Repository<T, D> : IRepository<T> where T : Entity where D : Dto<T>
{
    protected CookieSession _session;
    protected string _endpoint;

    public Repository(CookieSession session, string endpoint)
    {
        _session = session;
        _endpoint = endpoint;
    }
    
    public async Task<T[]> All(int page = 1)
    {
        var response = await _session.Request(_endpoint + "?page=" + page)
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<D[]>>();
        
        return json.Data.Select(dto => dto.ToEntity()).ToArray();
    }
    
    public async Task<T> ById(uint id)
    {
        var response = await _session.Request($"{_endpoint}/{id}")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<D>>();

        return json.Data.ToEntity();
    }
}
