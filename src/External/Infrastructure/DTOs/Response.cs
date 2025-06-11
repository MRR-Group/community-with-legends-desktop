using System.Text.Json.Serialization;
namespace Infrastructure.DTOs;

public record Response<T>
{
    [property: JsonPropertyName("data")]
    public T Data { get; set; }
}
