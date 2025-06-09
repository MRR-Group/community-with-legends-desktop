using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.DTOs;

public record AssetDto
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("type")]
    public string Type { get; set; }
    
    [property: JsonPropertyName("link")]
    public string Link { get; set; }

    public Asset? ToEntity()
    {
        try
        {
            return new Asset(Id, Enum.Parse<AssetType>(Type), new Uri(Link));
        }
        catch (UriFormatException _)
        {
            return null;
        }
    }
}