namespace Infrastructure.Exceptions;

public class FormValidationException : Exception
{
    public string Field { get; }     

    public FormValidationException(string field, string errorMessage) : base(errorMessage)
    {
        Field = field;        
    }
}