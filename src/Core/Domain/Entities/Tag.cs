using Domain.Primitives;
namespace Domain.Entities;

public class Tag : Entity
{
    public string Name { get; private set; }

    public Tag(uint id, string name) : base(id)
    {
        Name = name;
    }
}
