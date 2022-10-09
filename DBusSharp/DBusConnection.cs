using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBusSharp;

/// <summary>
/// Describes an address for a DBus Server
/// </summary>
public class DBusAddress
{
    public string AddressStr { get; set; }
    public string TransportName { get; set; } // should always be unix?
   public List<KeyValuePair<string, string>> KeyValuePairs = new List<KeyValuePair<string, string>>();

   public DBusAddress(string addressStr)
   {
       // Example
       // unix:path=/tmp/dbus-test
       this.AddressStr = addressStr;
       
       string[] addrParts = addressStr.Split(':');
       this.TransportName = addrParts[0];

       string[] keyValueParts = addrParts[1].Split(',');
       foreach (string keyValue in keyValueParts)
       {
           string[] split = keyValue.Split('=');
           this.KeyValuePairs.Add(new KeyValuePair<string, string>(split[0], split[1]));
       }
   }
}
public class DBusConnection: IDisposable
{
    private Router _router;
    private MessageFilterList _messageFilterList;
    private DBusProxy _busProxy;
    private Socket _dbusSocket;
    private List<DBusAddress> AvailableAddresses { get; set; } = new List<DBusAddress>();

    public DBusConnection()
    {
        GetDBusAddressList();
        _dbusSocket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
        var ep = new UnixDomainSocketEndPoint(this.AvailableAddresses[0].KeyValuePairs[0].Value);
        _dbusSocket.Connect(ep);
    }

    public void GetDBusAddressList(DBusAddressType addressType = DBusAddressType.Session)
    {
        if (addressType == DBusAddressType.Session)
        {
            foreach (string addr in this.GetSessionBusAddressString().Split(';'))
            {
                this.AvailableAddresses.Add(new DBusAddress(addr));
            }
        }else if (addressType == DBusAddressType.System)
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new InvalidDataException($"Address type {addressType} is not valid");
        }
    }

    private string GetSessionBusAddressString()
    {
        // >>> import os
        // >>> os.environ['DBUS_SESSION_BUS_ADDRESS']
        // 'unix:path=/run/user/1000/bus'
        //     >>> 
        //     KeyboardInterrupt
        //     >>> os.environ['DBUS_SESSION_BUS_ADDRESS']
        // 'unix:path=/run/user/1000/bus'
        //     >>> exit()
        string? sessionBusAddressStr = Environment.GetEnvironmentVariable("DBUS_SESSION_BUS_ADDRESS");
        if (sessionBusAddressStr == null)
        {
            throw new ExternalException($"No environment variable was found with the name DBUS_SESSION_BUS_ADDRESS");
        }

        return sessionBusAddressStr;
    }

    public DBusMessage? SendAndGetReply(DBusMessage message, TimeSpan timeout)
    {
        var messageBuffer = message.Serialize();
        _dbusSocket.Send(messageBuffer);
        while (true)
        {
            byte[] receiveBuffer = new byte[1024];
            _dbusSocket.Receive(receiveBuffer, SocketFlags.None);
            DBusMessage msg = new DBusMessage(receiveBuffer);
            if (msg.GetSerial() == message.GetSerial())
            {
                return msg;
            }
        }
        return null;
    }

    public void Dispose()
    {
        _dbusSocket.Close();
        _dbusSocket.Dispose();
    }
}

public enum DBusAddressType
{
    Session = 0,
    System = 1
}