using Application.Abstractions;
using Domain.Entities;
using Flurl.Http;
using Infrastructure.Exceptions;
namespace Infrastructure.Services;

public class RoleService : IRoleService
{
    private CookieSession _session;
    
    public RoleService(CookieSession session)
    {
        _session = session;
    }

    public async Task<bool> GiveModeratorRole(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/grant-moderator-privileges")
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
    
    public async Task<bool> RevokeModeratorRole(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/revoke-moderator-privileges")
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
    
    public async Task<bool> RevokeAdministratorRole(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/revoke-administrator-privileges")
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
