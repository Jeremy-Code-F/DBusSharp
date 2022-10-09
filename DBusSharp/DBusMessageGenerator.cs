namespace DBusSharp;

public class DBusMessageGenerator: IMessageGenerator
{
    public string Interface { get;  }= "org.freedesktop.DBus";

    public string ObjectPath { get; } = "/org/freedesktop/DBus";
    public string BusName { get; } = "org.freedesktop.DBus";

    public DBusMessage Hello()
    {
        return MessageGenerator.NewMethodCall(this, "Hello");
    }
}