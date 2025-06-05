using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.DTOs;

public record AssetDto : Dto<Asset>
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("type")]
    public string Type { get; set; }
    
    [property: JsonPropertyName("link")]
    public string Link { get; set; }

    public override Asset ToEntity()
    {
        var link = Link;
        
        return new Asset(Id, Enum.Parse<AssetType>(Type), new Uri(Link));
    }
}