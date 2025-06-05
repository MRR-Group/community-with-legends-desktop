using System;
namespace Presentation.Exceptions;

public class IncorrectYoutubeUrlException : Exception
{
    public IncorrectYoutubeUrlException() : base("Incorrect Youtube Url")
    {
    }
}
