namespace Infrastructure.Exceptions;

public class FormValidationException : Exception
{
    public Dictionary<string, List<string>> Fields { get; }     

    public FormValidationException(Dictionary<string, List<string>> fields) : base("Validation error")
    {
        Fields = fields;
    }
}