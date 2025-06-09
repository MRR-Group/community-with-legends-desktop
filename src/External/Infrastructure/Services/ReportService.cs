using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ReportService : IReportService
{
    private CookieSession _session;
    
    public ReportService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<bool> Close(Report report)
    {
        try
        {
            await _session.Request($"reports/{report.Id}/close")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostAsync();

            return true;
        }
        catch (FlurlHttpException e)
        {
            if (e.StatusCode == 403)
            {
                throw new UnauthorizedException();
            }

            throw;
        }
    }
    
    public async Task<bool> Reopen(Report report)
    {
        try
        {
            await _session.Request($"reports/{report.Id}/reopen")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostAsync();
            
            return true;
        }
        catch (FlurlHttpException e)
        {
            switch (e.StatusCode)
            {
                case 403:
                    throw new UnauthorizedException();
            }

            throw;
        }
    }
}