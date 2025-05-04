using System.Diagnostics;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Flurl;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AuthService : ILogInService, IRegisterService
{
    private CookieSession _session;
    private UserRepository _repository;
    
    public AuthService(CookieSession session, UserRepository repository)
    {
        _session = session;
        _repository = repository;
    }

    public async Task Register(string name, Email email, Password password)
    {
        try
        {
            await _session.Request("auth/register")
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
            var response = await _session.Request("auth/login")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { email = email.Value, password = password.Value });

            var json = await response.GetJsonAsync<LoginResponseDto>();
            return await _repository.ById(json.UserId);
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