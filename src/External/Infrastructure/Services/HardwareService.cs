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

public class HardwareService : IHardwareService
{
    private CookieSession _session;
    
    public HardwareService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<bool> DeleteHardware(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/hardware")
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
}