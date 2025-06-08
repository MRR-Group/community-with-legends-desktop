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

public class PostService : IPostService
{
    private CookieSession _session;
    
    public PostService(CookieSession session)
    {
        _session = session;
    }
    
    public override async Task Delete(Post post)
    {
        try
        {
            await _session.Request($"posts/{post.Id}")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .DeleteAsync();
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

    public override async Task Restore(Post post)
    {
        try
        {
            await _session.Request($"posts/{post.Id}/restore")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostAsync();
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