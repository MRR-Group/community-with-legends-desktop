using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public sealed record UserDto
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }

    [property: JsonPropertyName("roles")]
    public string[] Roles { get; set; }
    
    [property: JsonPropertyName("email")]
    public string Email { get; set; }
    
    [property: JsonPropertyName("name")]
    public string Name { get; set; }
    
    [property: JsonPropertyName("avatar")]
    public string Avatar { get; set; }
    
    [property: JsonPropertyName("permissions")]
    public string[] Permissions { get; set; }
    
    [property: JsonPropertyName("isBanned")]
    public bool IsBanned { get; set; }
    
    [property: JsonPropertyName("created_at")]
    public string CreationDate { get; set; }

    [property: JsonPropertyName("hardware")]
    public HardwareDto[]? HardwareList { get; set; }

    public User ToEntity()
    { 
        return new User(Id, Name, new Uri(Avatar), new Email(Email), IsBanned, ConvertRoles(), ConvertPermissions(), new Date(CreationDate));
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
