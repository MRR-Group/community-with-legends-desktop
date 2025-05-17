using Domain.Entities;

namespace Application.Abstractions;

using Domain.ValueObjects;

public interface ILogOutService
{
    public Task LogOut();
}