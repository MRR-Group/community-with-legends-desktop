using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.DTOs;

public record TagDto
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("name")]
    public string Name { get; set; }

    public Tag ToEntity()
    {
        return new Tag(Id, Name);
    }
}