using Domain.Entities;
namespace Application.Abstractions;

public abstract class IPostService
{
    public abstract Task Delete(Post post);
    public abstract Task Restore(Post post);
}
