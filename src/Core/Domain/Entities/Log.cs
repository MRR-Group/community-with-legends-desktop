using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class Log : Entity
{
    public string Description { get; private set; }
    public Date CreationDate { get; private set; }
    public User Causer { get; private set; }

    public Log(uint id, string description, Date creationDate, User causer) : base(id)
    {
        Description = description;
        CreationDate = creationDate;
        Causer = causer;
    }
}
