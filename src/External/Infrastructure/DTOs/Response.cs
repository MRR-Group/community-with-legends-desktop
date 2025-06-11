using System.Text.Json.Serialization;
namespace Infrastructure.DTOs;

public record Meta
{
    [property: JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }
    
    [property: JsonPropertyName("last_page")]
    public int LastPage { get; set; }
}

public record Response<T>
{
    [property: JsonPropertyName("data")]
    public T Data { get; set; }
    
    [property: JsonPropertyName("meta")]
    public Meta Meta { get; set; }
}
