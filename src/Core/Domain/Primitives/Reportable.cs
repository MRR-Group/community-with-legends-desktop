using Domain.Entities;
using Domain.ValueObjects;
namespace Domain.Primitives;

public class Reportable : Entity
{
    public string Content { get; private set; }
    public Date CreationDate { get; private set; }
    public User User { get; private set; }
    
    public Reportable(uint id, Date creationDate, User user, string content) : base(id)
    {
        CreationDate = creationDate;
        User = user;
        Content = content;
    }
}
