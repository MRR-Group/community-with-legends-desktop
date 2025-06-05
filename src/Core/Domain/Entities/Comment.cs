using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class Comment : Reportable
{
    public Comment(uint id, string content, Date creationDate, User user) : base(id, creationDate, user, content)
    {
    }
}
