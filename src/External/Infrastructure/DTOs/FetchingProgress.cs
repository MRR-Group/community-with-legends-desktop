using System.Text.Json.Serialization;

namespace Infrastructure.DTOs;

class FetchingProgress
{
    [property: JsonPropertyName("progress")]
    public int Progress { get; set; }
}