using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAdministratorService
{
    public Task<Administrator> CreateAdministrator(string name, Email email, Password password);
    public Task<bool> DeleteAdministrator(Administrator user);
    public Task<bool> RevokeAdministratorRole(User user);
}