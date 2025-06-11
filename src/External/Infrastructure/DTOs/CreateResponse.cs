using System.Text.Json.Serialization;
namespace Infrastructure.DTOs;

public record CreateResponse
{
    [property: JsonPropertyName("message")]
    public string Message { get; set; }
    
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
}
