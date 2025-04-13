using System.Diagnostics;
using Application.Abstractions;
using Domain.Entities;
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

public class AuthService : ILogInService, IRegisterService
{
    private Url _api;

    public AuthService(Url api)
    {
        this._api = api;
    }

    public async Task Register(string name, Email email, Password password)
    {
        try
        {
            await new Url(_api)
                .AppendPathSegment("auth/register")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { name, email = email.Value, password = password.Value });
        }
        catch (FlurlHttpException e)
        {
            if (e.StatusCode == 422)
            {
                var validation = await e.GetResponseJsonAsync<ValidationErrorsDto>();

                if (validation?.Errors != null)
                {
                    throw new FormValidationException(validation.Errors);
                }
            }

            throw;
        }
    }

    public async Task<User> LogIn(Email email, Password password)
    {
        try
        {
            var response = await new Url(_api)
                .AppendPathSegment("auth/login")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { email = email.Value, password = password.Value });
            
            var json = await response.GetJsonAsync<LoginResponseDto>();

            return json.User;
        }
        catch (FlurlHttpException e)
        {
            if (e.StatusCode == 422)
            {
                var validation = await e.GetResponseJsonAsync<ValidationErrorsDto>();

                if (validation?.Errors != null)
                {
                    throw new FormValidationException(validation.Errors);
                }
            }

            throw;
        }
    }
}