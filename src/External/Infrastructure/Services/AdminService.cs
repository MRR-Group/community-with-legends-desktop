using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AdminService : IAdministratorService
{
    private CookieSession _session;
    private AdminRepository _adminRepository;

    public AdminService(CookieSession session, AdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
        _session = session;
    }
    
    public async Task<bool> RevokeAdministratorRole(User user)
    {
        try
        {
            await _session.Request($"admins/{user.Id}/revoke-administrator-privileges")
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
    
    public async Task<Administrator> CreateAdministrator(string name, Email email, Password password)
    {
        try
        {
            var response = await _session.Request("admins")
                .WithAutoRedirect(true)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { name, email = email.Value, password = password.Value });

            var json = await response.GetJsonAsync<CreateResponse>();

            return await _adminRepository.ById(json.Id);
        }
        catch (FlurlHttpException e)
        {
            switch (e.StatusCode)
            {
                case 403:
                    throw new UnauthorizedException();
                
                case 422:
                    {
                        var validation = await e.GetResponseJsonAsync<ValidationErrorsDto>();

                        if (validation?.Errors != null)
                        {
                            throw new FormValidationException(validation.Errors);
                        }
                        break;
                    }
            }

            throw;
        }
    }

    public async Task<bool> DeleteAdministrator(Administrator administrator)
    {
        try
        {
            await _session.Request($"admins/{administrator.Id}")
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

        return true;
    }
}