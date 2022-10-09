// See https://aka.ms/new-console-template for more information

using DBusSharp;

using (DBusConnection connection = new DBusConnection())
{
    DBusMessageGenerator dbus = new DBusMessageGenerator();
    DBusMessage msg = dbus.Hello();
    var response = connection.SendAndGetReply(msg, TimeSpan.FromSeconds(20));
    Console.WriteLine();
    // var bytes = msg.Serialize();
    // Console.WriteLine();
}
Console.WriteLine();