namespace DBusSharp.Exceptions;

public class SignatureParsingException: Exception
{
    public SignatureParsingException()
    {
    }

    public SignatureParsingException(string message)
        : base(message)
    {
    }

    public SignatureParsingException(string message, Exception inner)
        : base(message, inner)
    {
    } 
    
}