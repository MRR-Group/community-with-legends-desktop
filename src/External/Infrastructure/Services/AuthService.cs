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
        var response = await api
            .AppendPathSegment("auth/register")
            .WithAutoRedirect(true)
            .PostJsonAsync(new { name, email, password });
        
        if (response.StatusCode != 422)
        {
            await HandleValidationErrors(response);
        }
        
        if (response.StatusCode != 201)
        {
            throw new Exception("Failed to sign in, please try again later.");
        }

        var res = response;
    }

    private async Task HandleValidationErrors(IFlurlResponse response)
    {
        var validation = await response.GetJsonAsync<ValidationErrorsDto>();
        
        if (validation?.Errors != null)
        {
            foreach (var error in validation.Errors)
            {
                foreach (var errorMessage in error.Value)
                {
                    throw new FormValidationException(error.Key, errorMessage);
                }
            }
        }
    }
}