namespace DBusSharp;

// https://gitlab.com/takluyver/jeepney/-/blob/master/jeepney/wrappers.py#L126
public interface IMessageGenerator
{
    public string Interface { get; }
    public string ObjectPath { get; }
    public string BusName { get; }
}