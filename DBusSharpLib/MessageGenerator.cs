namespace DBusSharp;

public class MessageGenerator
{
    private static uint _currentSerial = 0;
    public static DBusMessage NewMethodCall(IMessageGenerator messageGenerator, string methodName, string? signature=null, MessageBody? messageBody=null)
    {
        MessageHeader messageHeader = new MessageHeader(MessageType.MethodCall);
        messageHeader.HeaderFields.Add(new MessageHeaderField(FieldCodeEnum.Path, messageGenerator.ObjectPath));
        messageHeader.HeaderFields.Add(new MessageHeaderField(FieldCodeEnum.Destination, messageGenerator.BusName));
        messageHeader.HeaderFields.Add(new MessageHeaderField(FieldCodeEnum.Interface, messageGenerator.Interface));
        messageHeader.HeaderFields.Add(new MessageHeaderField(FieldCodeEnum.Member, methodName));
        if (signature != null)
        {
            messageHeader.HeaderFields.Add(new MessageHeaderField(FieldCodeEnum.Signature, signature));
        }

        return new DBusMessage(messageHeader, null);
    }

    public static uint GetNextSerial()
    {
        return _currentSerial += 1;
    }
}