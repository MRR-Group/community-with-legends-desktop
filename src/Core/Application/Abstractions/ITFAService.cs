using Domain.Entities;

namespace Application.Abstractions;

public interface ITFAService
{
    public Task Validate(string token);
    public Task<bool> Check();
}