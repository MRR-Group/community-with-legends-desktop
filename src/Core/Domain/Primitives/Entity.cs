namespace Domain.Primitives;

public abstract class Entity
{
    protected Entity(uint id) => Id = id;
    
    public uint Id { get; protected set; }
}