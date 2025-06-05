using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Administrator : User
{
    public Administrator(uint id, string name, Uri avatar, Email email, Roles roles, Permissions permissions, Date creationDate) : base(id, name, avatar, email, roles, permissions, creationDate)
    {
    }
}