using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class ReportRepository
{
    private CookieSession _session;

    public ReportRepository(CookieSession session)
    {
        _session = session;
    }

    public async Task<Report[]> All()
    {
        var response = await _session.Request("reports")
            .WithAutoRedirect(true)
            .WithHeader("Accept", "application/json")
            .GetAsync();
        
        var json = await response.GetJsonAsync<Response<ReportDto[]>>();

        return json.Data.Select(dto => dto.ToEntity()).ToArray();
    }
}
