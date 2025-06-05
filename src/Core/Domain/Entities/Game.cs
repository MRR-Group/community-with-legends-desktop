using Domain.Primitives;
namespace Domain.Entities;

public class Game : Entity
{
    public string Name { get; private set; }
    public Uri Cover { get; private set; }

    public Game(uint id, string name, Uri cover) : base(id)
    {
        Name = name;
        Cover = cover;
    }
}
