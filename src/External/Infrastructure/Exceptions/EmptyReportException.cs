namespace Infrastructure.Exceptions;

public class EmptyReportException : Exception
{
    public EmptyReportException() : base("Empty report")
    {
    }
}
