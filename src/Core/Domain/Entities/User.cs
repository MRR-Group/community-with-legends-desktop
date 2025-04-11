using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    
    internal User(uint id, string name, Email email) : base(id)
    {
        Name = name;
        Email = email;
    }
}