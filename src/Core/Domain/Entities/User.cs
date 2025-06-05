using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public Uri Avatar { get; private set; }
    public Email Email { get; private set; }
    public Roles Roles { get; private set; }
    public Permissions Permissions { get; private set; }
    public Date CreationDate { get; private set; }
    
    public User(uint id, string name, Uri avatar, Email email, Roles roles, Permissions permissions, Date creationDate) : base(id)
    {
        Name = name;
        Avatar = avatar;
        Email = email;
        Roles = roles;
        Permissions = permissions;
        CreationDate = creationDate;
    }
}