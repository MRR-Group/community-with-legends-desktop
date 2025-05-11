using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public sealed record UserDto
{
    public uint Id { get; set; }
    
    public string[] Roles { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string Avatar { get; set; }
    
    public string[] Permissions { get; set; }
    
    [property: JsonPropertyName("created_at")]
    public string CreationDate { get; set; }

    public User ToEntity()
    {
        return new User(Id, Name, Avatar, new Email(Email), ConvertRoles(), ConvertPermissions(), new Date(CreationDate));
    }

    private Roles ConvertRoles()
    {
        return new Roles(Roles.Select(FirstCharToUpper).Select(Enum.Parse<Role>).ToArray());
    }
    
    private Permissions ConvertPermissions()
    {
        return new Permissions(Permissions.Select(FirstCharToUpper).Select(Enum.Parse<Permission>).ToArray());
    }
    
    private string FirstCharToUpper(string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : $"{char.ToUpper(input[0])}{input[1..]}";
    }
}
