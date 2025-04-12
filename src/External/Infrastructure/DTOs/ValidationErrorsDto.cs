namespace Infrastructure.DTOs;

public class ValidationErrorsDto
{
    public Dictionary<string, List<string>> Errors { get; }
    
    public ValidationErrorsDto(Dictionary<string, List<string>> errors)
    {
        Errors = errors;
    }
}