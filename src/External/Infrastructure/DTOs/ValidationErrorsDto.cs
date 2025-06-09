using System.Text.Json.Serialization;
namespace Infrastructure.DTOs;

public class ValidationErrorsDto
{
    [property: JsonPropertyName("errors")]
    public Dictionary<string, List<string>> Errors { get; }
    
    public ValidationErrorsDto(Dictionary<string, List<string>> errors)
    {
        Errors = errors;
    }
}