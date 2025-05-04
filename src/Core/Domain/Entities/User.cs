using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public string Avatar { get; private set; }
    public Email Email { get; private set; }
    public Roles Roles { get; private set; }
    public Permissions Permissions { get; private set; }
    public Date ModificationDate { get; private set; }

    public User(uint id, string name, string avatar, Email email, Roles roles, Permissions permissions, Date modificationDate) : base(id)
    {
        Name = name;
        Avatar = avatar;
        Email = email;
        Roles = roles;
        Permissions = permissions;
        ModificationDate = modificationDate;
    }
}