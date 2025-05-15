using Domain.Enums;
namespace Domain.ValueObjects;

public sealed record Roles
{
    public Role[] Value { get; private set; }
    
    public Roles(Role[] roles)
    {
        Value = roles;
    }
    
    public static implicit operator Role[] (Roles roles) => roles.Value;
        
    public static implicit operator Roles(Role[] value) => new Roles(value);
    
    public bool Is(Role role) => Value.Contains(role);

    public bool IsAdministrator { get => Is(Role.Administrator) || Is(Role.SuperAdministrator); }
    public bool IsSuperAdministrator { get => Is(Role.SuperAdministrator); }
    public bool IsModerator { get => Is(Role.Moderator); }
}
