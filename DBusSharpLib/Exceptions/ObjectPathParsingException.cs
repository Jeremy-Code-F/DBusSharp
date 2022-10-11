namespace DBusSharp.Exceptions;

public class ObjectPathParsingException: Exception
{
    public ObjectPathParsingException()
    {
    }

    public ObjectPathParsingException(string message)
        : base(message)
    {
    }

    public ObjectPathParsingException(string message, Exception inner)
        : base(message, inner)
    {
    } 
}