using Domain.Enums;
namespace Domain.ValueObjects;

public sealed record Permissions
{
    public Permission[] Value { get; private set; }
    
    public Permissions(Permission[] permissions)
    {
        Value = permissions;
    }
    
    public static implicit operator Permission[] (Permissions permissions) => permissions.Value;
        
    public static implicit operator Permissions(Permission[] value) => new Permissions(value);
    
    public bool Can(Permission permission) => Value.Contains(permission);
    public bool Cannot(Permission permission) => !Can(permission);
}
