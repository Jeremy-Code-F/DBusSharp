namespace DBusSharp;

public class DBusProxy
{
    private IMessageGenerator _messageGenerator;
    private DBusConnection _connection;

    public DBusProxy(IMessageGenerator messageGenerator, DBusConnection connection)
    {
        _messageGenerator = messageGenerator;
        _connection = connection;
    }
}