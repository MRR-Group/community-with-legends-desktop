using System.Text.Json.Serialization;

namespace Infrastructure.DTOs;

public record LoginResponseDto
{
    [property: JsonPropertyName("message")]
    public string Message { get; set; }
    
    [property: JsonPropertyName("user_id")]
    public uint UserId { get; set; }
}