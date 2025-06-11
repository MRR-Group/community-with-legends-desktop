using Domain.Primitives;
namespace Application.Abstractions;

public interface IRepository<T> where T : Entity
{
    public Task<T[]> All(int page = 1);
    public Task<T> ById(uint id);
}
