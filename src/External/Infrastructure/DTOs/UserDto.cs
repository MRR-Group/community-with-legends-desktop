using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public sealed record UserDto
{
    public uint Id { get; private set; }
    
    public string[] Roles { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string Avatar { get; set; }
    
    public string[] Permissions { get; set; }
    
    public uint ModificationDate { get; set; }

    public User ToEntity()
    {
        return new User(Id, Name, Avatar, new Email(Email), ConvertRoles(), ConvertPermissions(), new Date(ModificationDate));
    }

    private Roles ConvertRoles()
    {
        return new Roles(Roles.Select(Enum.Parse<Role>).ToArray());
    }
    
    private Permissions ConvertPermissions()
    {
        return new Permissions(Permissions.Select(Enum.Parse<Permission>).ToArray());
    }
}
