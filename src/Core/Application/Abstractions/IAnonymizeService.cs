using Domain.Entities;

namespace Application.Abstractions;

public interface IAnonymizeService
{
    public Task<bool> Anonymize(User user);
}