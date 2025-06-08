using Domain.Entities;
namespace Application.Abstractions;

public abstract class ICommentService
{
    public abstract Task Delete(Comment comment);
    public abstract Task Restore(Comment comment);

}
