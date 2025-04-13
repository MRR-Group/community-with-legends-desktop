using System.Diagnostics;
using Application.Abstractions;
using Domain.ValueObjects;
using Flurl;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;

namespace Infrastructure.Services;

public record Message
{
    public string? message;
}

public class AuthService : ISignInService
{
    private Url api;

    public AuthService(Url api)
    {
        this.api = api;
    }

    public async Task SignIn(string name, Email email, Password password)
    {
        
        try
        {
            var response = await new Url(api)
                .AppendPathSegment("auth/register")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { name, email = email.Value, password = password.Value });
        }
        catch (FlurlHttpException e)
        {
            if (e.StatusCode == 422)
            {
                await HandleValidationErrors(e);
            }
            else
            {
                throw;
            }
        }
    }

    private async Task HandleValidationErrors(FlurlHttpException e)
    {
        var validation = await e.GetResponseJsonAsync<ValidationErrorsDto>();
        
        if (validation?.Errors != null)
        {
            throw new FormValidationException(validation.Errors);
        }
    }
}