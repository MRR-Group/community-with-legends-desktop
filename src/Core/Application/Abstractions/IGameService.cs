using Domain.Entities;

namespace Application.Abstractions;

public interface IGameService
{
    public Task<int> Fetch();

    public Task<int> Progress();
}