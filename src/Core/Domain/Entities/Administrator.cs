using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Administrator : User
{
    public Administrator(uint id, string name, Uri avatar, Email email, bool isBanned, Roles roles, Permissions permissions, Date creationDate) : base(id, name, avatar, email, isBanned, roles, permissions, creationDate)
    {
    }
}