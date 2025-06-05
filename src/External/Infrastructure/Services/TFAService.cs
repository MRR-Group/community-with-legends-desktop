using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class TFAService : ITFAService
{
    private CookieSession _session;
    
    public TFAService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task Validate(string token)
    {
        try
        {
            await _session.Request("auth/tfa/validate")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { token });
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
    
    public async Task<bool> Check()
    {
        try
        {
            await _session.Request("auth/tfa")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .GetAsync();

            return true;
        }
        catch (FlurlHttpException _)
        {
            return false;
        }
    }
}