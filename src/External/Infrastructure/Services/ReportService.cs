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
    
    public async Task<bool> Delete(Reportable reportable)
    {
        try
        {
            await _session.Request($"reports/{reportable.Id}/close")
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
    
    public async Task<bool> Restore(Reportable reportable)
    {
        try
        {
            await _session.Request($"reports/{reportable.Id}/reopen")
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