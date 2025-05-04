using System.Text.Json.Serialization;
using Domain.Entities;

namespace Infrastructure.DTOs;

public record LoginResponseDto
{
    public string Message { get; set; }
    
    [property: JsonPropertyName("user_id")]
    public uint UserId { get; set; }
}