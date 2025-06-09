using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ProfileService : IProfileService
{
    private CookieSession _session;
    
    public ProfileService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<bool> DeleteAvatar(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/avatar")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .DeleteAsync();

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
    
    public async Task<bool> Rename(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/name")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .DeleteAsync();
            
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