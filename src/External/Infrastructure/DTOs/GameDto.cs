using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.DTOs;

public record GameDto : Dto<Game>
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("name")]
    public string Name { get; set; }
    
    [property: JsonPropertyName("cover")]
    public string Cover { get; set; }

    public override Game ToEntity()
    {
        return new Game(Id, Name, new Uri(Cover));
    }
}