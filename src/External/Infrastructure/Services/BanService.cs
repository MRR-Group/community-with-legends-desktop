using Application.Abstractions;
using Domain.Entities;
using Flurl.Http;
using Infrastructure.Exceptions;

namespace Infrastructure.Services;

public class BanService : IBanService
{
    private CookieSession _session;
    
    public BanService(CookieSession session)
    {
        _session = session;
    }

    public async Task<bool> Ban(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/ban")
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

    public async Task<bool> Unban(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/unban")
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
}