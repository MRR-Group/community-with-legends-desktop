using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.ValueObjects;
namespace Infrastructure.DTOs;

public record LogDto : Dto<Log>
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("description")]
    public string Description { get; set; }
    
    [property: JsonPropertyName("causer")]
    public UserDto Causer { get; set; }
    
    [property: JsonPropertyName("created_at")]
    public string CreationDate { get; set; }

    public override Log ToEntity()
    {
        var causer = Causer.ToEntity();
        
        return new Log(Id, Description, new Date(CreationDate), causer);
    }
}
