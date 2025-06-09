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

public class CommentService : ICommentService
{
    private CookieSession _session;
    
    public CommentService(CookieSession session)
    {
        _session = session;
    }
    
    public override async Task Delete(Comment comment)
    {
        try
        {
            await _session.Request($"comments/{comment.Id}")
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

    public override async Task Restore(Comment comment)
    {
        try
        {
            await _session.Request($"comments/{comment.Id}/restore")
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