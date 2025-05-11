using Application.Abstractions;
using Domain.Entities;
using Flurl.Http;
using Infrastructure.Exceptions;
namespace Infrastructure.Services;

public class AnonymizeService : IAnonymizeService
{
    private CookieSession _session;
    
    public AnonymizeService(CookieSession session)
    {
        _session = session;
    }
    
    public async Task<bool> Anonymize(User user)
    {
        try
        {
            await _session.Request($"users/{user.Id}/anonymize")
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
