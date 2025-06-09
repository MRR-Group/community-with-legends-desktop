using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.DTOs;

public record HardwareDto : Dto<Hardware>
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("title")]
    public string Title { get; set; }
    
    [property: JsonPropertyName("value")]
    public string Value { get; set; }

    public override Hardware ToEntity()
    {
        return new Hardware(Id, Title, Value);
    }
}
