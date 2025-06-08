namespace Application.Exceptions;

public class UnsupportedReportableTypeException : Exception
{
    public UnsupportedReportableTypeException(object reportable) 
        : base($"Unsupported reportable type: {reportable?.GetType().Name}") { }
}